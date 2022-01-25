using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Project.Common.GlobalError;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Common.Error
{
    public static class ExceptionExtension
    {
        public static void ConfigGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    if (contextPathFeature != null)
                    {
                        Log.Error($"Something Went Wrong at {contextPathFeature.Path}, Error: {contextPathFeature.Error}");
                        await context.Response.WriteAsync(new GlobalErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error. Please try Again Later."
                        }.ToString());
                    }
                });
            });
        }
    }
}
