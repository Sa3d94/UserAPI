using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Entity.Models;
using IOC;

namespace UserAPI.Extensions
{

    public static class ServiceExtension
    {
    //public static void RegisterHttpContext(this IServiceCollection services)
    //{
    //  services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
    //  services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    //}

    public static void RegisterServices(this IServiceCollection services)
    {
      DependencyContainer.RegisterServices(services);
    }
    public static void ConfigureDbContext(this IServiceCollection services, string ConnectionString)
      {
        services.AddDbContext<dbContext>(options => {
        options.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
      });
    }
      public static void ConfigureCors(this IServiceCollection services, string policy, string origin)
      {

        services.AddCors(options =>
        {
          options.AddPolicy(name: policy,
                            builder =>
                            {
                                       builder.AllowAnyOrigin()
                                       .AllowAnyHeader()
                                       .AllowAnyMethod()
                                       .SetIsOriginAllowed((host) => true);
                            });


        });
      }

    public static void ConfigureAuthentication(this IServiceCollection services , string Issuer, string Audience, string Key)
    {
      services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = Issuer,
          ValidAudience = Audience,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key))

        };



      });
    }

  }

  
}

