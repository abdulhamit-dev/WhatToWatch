using Microsoft.AspNetCore.Builder;

namespace WhatToWatch.Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void CustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
