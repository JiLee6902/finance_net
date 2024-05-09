using API_SYSTEM.Commons;
using System.Net;
using System.Text.Json;

namespace API_SYSTEM.Middleware
{
    public class GlobalException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalException> _logger;
        private readonly IHostEnvironment _env;

        public GlobalException(RequestDelegate next, ILogger<GlobalException> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() ?
                    new ApiException
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message,
                        Detail = ex.StackTrace?.ToString()
                    } :
                    new ApiException
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = ex.Message,
                        Detail = "Internal Server Error"
                    };
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = System.Text.Json.JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
