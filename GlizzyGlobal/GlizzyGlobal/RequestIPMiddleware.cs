using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace GlizzyGlobal
{
    public class RequestIPMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestIPMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string response;
            using (var client = new HttpClient())
            {
                response = await client.GetStringAsync("https://ipinfo.io");  
            }
            var json = JsonDocument.Parse(response);

            var ip = json.RootElement.GetProperty("ip").GetString();
            var city = json.RootElement.GetProperty("city").GetString();
            var region = json.RootElement.GetProperty("region").GetString();
            
            await context.Response.WriteAsync($"I know where you live..{city}, {region} \n{ip}");
        }
    }

    public static class RequestPMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestIP(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestIPMiddleware>();
        }
    }
}
