using GringottsBankingApp.API.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace GringottsBankingApp.API.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();

                    if (error != null)
                    {
                        var exception = error.Error;
                        ErrorDto errorDto = new();
                        errorDto.Status = 500;
                        errorDto.Errors.Add(exception.Message);

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorDto));
                    }
                });
            });
        }
    }
}
