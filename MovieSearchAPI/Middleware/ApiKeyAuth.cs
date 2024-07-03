using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MovieSearchAPI.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieSearchAPI.Middleware
{
    public class ApiKeyAuth
    {
        private readonly RequestDelegate _next;
        private readonly IList<string> _apiKeys;

        public ApiKeyAuth(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKeys = configuration.GetSection(Constants.ApiKeys).Get<List<string>>();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/admin"))
            {
                if (!context.Request.Headers.TryGetValue(Constants.ApiKeyHeader, out var apiKey))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("API Key not provided");
                    return;
                }

                if (!_apiKeys.Contains(apiKey.ToString()))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid API Key");
                    return;
                }
            }

            await _next(context);
        }
    }
}
