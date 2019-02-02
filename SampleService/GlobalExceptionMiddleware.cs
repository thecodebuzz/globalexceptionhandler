using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace SampleService
{
    public static class GlobalExceptionMiddleware
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if(contextFeature!=null)
                    {
                        //log the error
                        var logger = loggerFactory.CreateLogger("GlobalException");
                        logger.LogError($"Something went wrong: { contextFeature.Error}");

                        //report the error to client 
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCodeContext = context.Response.StatusCode,
                            Message ="Something Went Wrong.Please try again later"


                        }.ToString());
                    }

                });

             });

        }
    }
}
