using System;
using DataAccess.Interfaces;
using DataAccess.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IOC
{
  public class DependencyContainer
  {
    public static void RegisterServices(IServiceCollection services)
    {
      services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
      services.AddScoped<IUsersRepository, UsersRepository>();
      services.AddScoped<IEncryptService, EncryptService>();
      services.AddScoped<IAuthService, AuthService>();


    }
  }
}

