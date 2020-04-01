using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.API.Middleware
{
    public class ValidationExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException validationException)
            {
                var errorObject = new { 
                    Errors = validationException.Errors.Select(x => x.ErrorMessage)
                };

                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorObject), Encoding.UTF8);
            }
        }
    }
}
