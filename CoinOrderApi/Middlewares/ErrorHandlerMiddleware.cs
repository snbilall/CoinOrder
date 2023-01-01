using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace CoinOrderApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                if (error is ValidationException)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var msg = error.Message;
                    var resp = JsonSerializer.Serialize(new { message = msg });
                    await response.WriteAsync(resp);
                }
            }
        }
    }
}
