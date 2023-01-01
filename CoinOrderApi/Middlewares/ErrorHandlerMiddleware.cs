﻿using CoinOrderApi.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace CoinOrderApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
            ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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
                string? respMsg;
                if (error is ValidationException)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var msg = error.Message;
                    respMsg = JsonSerializer.Serialize(new { message = msg });
                }
                else if (error is UserHasActiveOrderException)
                {
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    respMsg = JsonSerializer.Serialize(new { message = "User has active order" });
                }
                else
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(error, "System Exception");
                    respMsg = JsonSerializer.Serialize(new { message = "System Exception, Please try again later" });
                }
                await response.WriteAsync(respMsg);

            }
        }
    }
}
