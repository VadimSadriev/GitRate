using System;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using GitRate.Common.Extensions;
using GitRate.Web.Common.Contracts.Exception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GitRate.Web.Common.Middlewares
{
    /// <summary>
    /// Handles all exceptions occured during web request
    /// </summary>
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMapper _mapper;

        public ErrorMiddleware(RequestDelegate next, IMapper mapper)
        {
            _next = next;
            _mapper = mapper;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleAsync(httpContext, ex);
            }
        }

        private async Task HandleAsync(HttpContext httpContext, Exception ex)
        {
            var code = StatusCodes.Status400BadRequest;

            switch (ex)
            {
                // to be continued
            }

            httpContext.Response.StatusCode = code;
            httpContext.Response.ContentType = MediaTypeNames.Application.Json;

            var exception = _mapper.Map<ExceptionContract>(ex);

            var result = exception.Serialize();

            await httpContext.Response.WriteAsync(result);
        }
    }

    public static class ErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}