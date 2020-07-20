using AutoMapper;
using BookStore.API.Middleware;
using BookStore.ApplicationServices.Handlers.Queries;
using BookStore.ApplicationServices.Interfaces;
using BookStore.ApplicationServices.Services;
using BookStore.ApplicationServices.Services.Messaging;
using BookStore.Caching.IOC;
using BookStore.DocumentParser.Interfaces;
using BookStore.DocumentParser.PdfParsers;
using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Elasticsearch.CommandObjects.Books;
using BookStore.Elasticsearch.Connection;
using BookStore.Elasticsearch.Interfaces;
using BookStore.Elasticsearch.QueryObjects.Books;
using BookStore.Elasticsearch.Repositories;
using BookStore.Messaging.Connection;
using BookStore.ServiceModel.Dtos;
using BookStore.ServiceModel.Validation;
using BookStore.SignalR.Hubs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
namespace BookStore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                //TODO: Think about a more appropriate restriction policy
                options.AddPolicy("CorsPolicy",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .DisallowCredentials();
                    });
            });

            services.AddControllers();

            services.AddAutoMapper(config =>
            {
                config.CreateMap<Book, BookDto>().ReverseMap();
                config.CreateMap<OperationResult, OperationResultDto>().ReverseMap();

            }, typeof(Startup));

            services.AddMediatR(typeof(GetBooksQueryHandler).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(UpsertBookValidator).Assembly);


            services.AddTransient<IElasticRepository, ElasticRepository>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookRepository, ElasticBookRepository>();
            services.AddTransient<IFileService>(x => new BlobFileService("UseDevelopmentStorage=true"));
            services.AddTransient<IPdfParser, PdfParser>();


            services.AddRabbitMQ(Configuration["RabbitMq:Host"]);
            services.AddTransient<IMessageProducerService, MessageProducerService>();
            services.AddHostedService<BookUpsertSubscriberService>();
            services.AddHostedService<BookUpsertedSubscriberService>();


            var elasticHosts = Configuration.GetSection("Elasticsearch:Hosts")
                .AsEnumerable()
                .Where(x => x.Value != null)
                .Select(x => x.Value);

            services.AddSingleton(x =>
            ElasticClientFactory.CreateElasticClient(elasticHosts, Configuration["Elasticsearch:Username"], Configuration["Elasticsearch:Password"]));
            services.AddTransient<IEnumerable<IElasticQuery>>(x => new List<IElasticQuery> {
            new GetBooksQuery()
            });

            services.AddTransient<IEnumerable<IElasticCommand>>(x => new List<IElasticCommand> {
            new UpsertBookCommand()
            });

            services.AddSignalR();


            var redisHosts = Configuration.GetSection("Redis:Hosts")
                .AsEnumerable()
                .Where(x => x.Value != null)
                .Select(x => x.Value);

            services.UseRedisCaching(redisHosts, uint.Parse(Configuration["Redis:Database"]));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    "v1",
                    info: new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Book Store",
                        Version = "1.0",
                        Description = "Book Store"
                    }
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ValidationExceptionHandler>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notification");
            });


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Book Store Api v1.0");
            });
        }
    }
}
