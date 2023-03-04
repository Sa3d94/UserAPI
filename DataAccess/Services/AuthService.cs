using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAccess.Dtos;
using DataAccess.Interfaces;
using Entity.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Services
{
  public class AuthService : IAuthService
  {
    readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
      this._configuration = configuration;
    }

    public  CreatedUser CreateToken(UserDto user)
    {
      CreatedUser createdUser = new CreatedUser
      {
        Id = user.Id,
        AccessToken = this.GenerateJwtToken(user)
      };

      return createdUser;
    }


    private string GenerateJwtToken(UserDto user)
    {
      var issuer = _configuration.GetValue<string>("Jwt:Issuer");
      var audience = _configuration.GetValue<string>("Jwt:Audience");

      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:AccessTokenSecret")));

      var signingCrednetials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
      List<Claim> claims = new List<Claim>
                      {
                          new Claim (ClaimTypes.Name, user.FirstName + " " + user.LastName),
                  
                      };

      var tokenOptions = new JwtSecurityToken(
          issuer: issuer,
          audience: audience,
          claims: claims,
          expires: DateTime.UtcNow.AddHours(3),
          signingCredentials: signingCrednetials
        );

      var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
      return tokenString;
    }
  }
}

