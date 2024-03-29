﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Searcher.BLL.DTO;
using System.Net;

namespace Searcher.Middleware
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        loggerFactory.CreateLogger("GlobalException")
                            .LogError($"Inner exception: {contextFeature.Error}");

                        await context.Response.WriteAsync(new ErrorResponse(context.Response.StatusCode, contextFeature.Error.ToString()).ToString());
                    }
                });
            });
        }
    }
}
