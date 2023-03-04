using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using DataAccess.Dtos;
using DataAccess.Interfaces;
using Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using static Microsoft.AspNetCore.Http.Results;

namespace UserAPI.Handlers
{
  public static class Users
  {

    internal static async Task<IResult> GetUsers(string id , IUsersRepository userRepository, IHttpContextAccessor _httpContextAccessor)

    {
     var _userID = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type.Contains("upn")).FirstOrDefault().Value;

      if (string.IsNullOrEmpty(id)) return BadRequest();

      // Query The User from the Database
      User user = await userRepository.GetUser(id);

      if (user is null) return NotFound("User is not found!");

      // Check if the User is querying his oqn data or not
      // From Claims

      if (user.Id != _userID) return Unauthorized();


      dynamic result;
      if (user.MarketingConsent)
      {
        result = new UserDto
        {
          Id = user.Id,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Email = user.Email,
          MarketingConsent = user.MarketingConsent
        };
      }
      else
      {
        result = new UserNoConsent
        {
          Id = user.Id,
          FirstName = user.FirstName,
          LastName = user.LastName,
          MarketingConsent = user.MarketingConsent
        };

      }


      return Ok(result);
    }

    internal static async Task<IResult> AddUser( UserDto user , IUsersRepository userRepository, IEncryptService encryptService , IAuthService authService)
    {

      if (user is null) return BadRequest();

      // Validation for all data annotation attributes 
      ICollection<ValidationResult> results;
      if (!Validate(user, out results))
      {
       return BadRequest(String.Join("\n", results.Select(o => o.ErrorMessage)));
      }

      // Check if the email already exists or not
      var userExists = await userRepository.GetUserByEmail(user.Email);
      if (userExists != null)
        return Conflict("Invalid `email`: A user with this email address already exists.");

      // Create an ID
      user.Id = encryptService.ComputeHash(user.Email);

      User newUser = new User {
        Id = user.Id,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Email = user.Email,
        MarketingConsent = user.MarketingConsent
      };

      // Create user and save to database
     var createdUser =  await userRepository.AddUser(newUser);

      if (createdUser == null)
      {
        return BadRequest("An error occurred!");
      }

      // Create a Token and return
      var response = authService.CreateToken(user);

      return Created("/user", response);


    }




      // Validator for Data Annotations
      private static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
    {
      results = new List<ValidationResult>();

      return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
    }

  }
}

