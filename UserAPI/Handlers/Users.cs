using System;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.Results;

namespace UserAPI.Handlers
{
  public static class Users
  {

    internal static async Task<IResult> GetUsers(string id)
    {
      return Ok("Test");
    }
    internal static async Task<IResult> AddUser()
    {
      return Ok("Test");
      //if (user is null) return BadRequest();
      //if (!MiniValidator.TryValidate(user, out var errors)) return BadRequest(errors);

      //if (users.Users.Any(u => u.Email == user.Email))
      //  return Conflict("Invalid `email`: A user with this email address already exists.");

      //if (string.IsNullOrWhiteSpace(user.Username))
      //{
      //  user.Username = string.Join('_', user.FullName.Split(' ')).ToLower();
      //  if (users.Users.Any(u => u.UserName == user.Username))
      //    user.Username = user.Username + '_' + Guid.NewGuid().ToString("N").Substring(0, 4);
      //}
      //else if (users.Users.Any(u => u.UserName == user.Username))
      //  return Conflict("Invalid `username`: A user with this username already exists.");

      //var newser = new User(user);
      //var result = await users.CreateAsync(newser, user.Password);
      //if (!result.Succeeded) return BadRequest(result.Errors);

      //return Created($"/users/{newser.Id}", newser);
    }

  }
}

