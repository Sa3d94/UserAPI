using System;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace UserAPI.Extensions
{
  public static class ExceptionMiddleware
  {
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
      app.UseExceptionHandler(appError =>
      {
        appError.Run(async context =>
        {
          var result = JsonSerializer.Serialize(new { error = "An Error occurred!" });

          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          context.Response.ContentType = "application/json";
          var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
          if (contextFeature != null)
          {
            await context.Response.WriteAsync(result);
          }
        });

      });
    }
  }
}

