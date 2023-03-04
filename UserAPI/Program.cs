using System.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;
using UserAPI.Extensions;
using UserAPI.Handlers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
  options.AddSecurityDefinition(
    "Bearer",
    new OpenApiSecurityScheme
    {
      Name = "Authorization",
      Type = SecuritySchemeType.ApiKey,
      In = ParameterLocation.Header,
      Scheme = "Bearer",
      BearerFormat = "JWT",
      Description =
        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
    }
  );

  options.AddSecurityRequirement(
    new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          }
        },
        new string[] { }
      }
    }
  );
});


// Configure DI Coontainer
builder.Services.RegisterServices();


// Configure the Database Context
builder.Services.ConfigureDbContext(builder.Configuration.GetValue<string>("Settings:ConnectionStrings:DefaultConnection"));

// Configure Cors Policy
builder.Services.ConfigureCors(builder.Configuration["CorsPolicyName"], builder.Configuration.GetValue<string>("Settings:CoreOrigin"));

// Configure JWT
builder.Services.ConfigureAuthentication(builder.Configuration["Jwt:Issuer"] , builder.Configuration["Jwt:Audience"] , builder.Configuration["Jwt:AccessTokenSecret"]);

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure Exception Middleware
app.ConfigureExceptionHandler();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseCors(builder.Configuration["CorsPolicyName"]);

app.UseAuthorization();

app.MapGet("user/{id}", Users.GetUsers).RequireAuthorization();

app.MapPost("user", Users.AddUser);

app.Run();



