using ChannelBot.Utilities.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.Utilities.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ErrorException ex)
            {
                _logger.LogError(ex, ex.StackTrace);
                await HandleSoftExceptionAsync(httpContext, ex);
            }
            catch (FriendlyException ex)
            {
                _logger.LogError(ex, ex.StackTrace);
                await HandleUserFriendlyExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleSoftExceptionAsync(HttpContext context, ErrorException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 400;

            return context.Response.WriteAsync(exception.ToString());
        }

        private Task HandleUserFriendlyExceptionAsync(HttpContext context, FriendlyException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 204; //no content

            return context.Response.WriteAsync(exception.ToString());
        }


    }
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
