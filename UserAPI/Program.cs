using Microsoft.OpenApi.Models;
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapGet("user/{id}", Users.GetUsers);

app.MapPost("user", Users.AddUser);

app.Run();



