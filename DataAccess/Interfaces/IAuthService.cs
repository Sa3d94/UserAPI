using System;
using DataAccess.Dtos;
using Entity.Models;

namespace DataAccess.Interfaces
{
  public interface IAuthService
  {
    CreatedUser CreateToken(UserDto user);


  }
}

