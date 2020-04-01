using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Messaging.Connection
{
    public static class RabbitServiceCollectionExtensions
    {
        public static void AddRabbitMQ(this IServiceCollection services, string url)
        {
            services.AddTransient(x =>
            {
                return new RabbitConnectionFactory().CreateConnection(url);
            });
        }
    }
}
