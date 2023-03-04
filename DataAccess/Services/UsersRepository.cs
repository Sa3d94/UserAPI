using System;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Entity.Models;

namespace DataAccess.Services
{
  public class UsersRepository : Repository<User>, IUsersRepository
  {
    public UsersRepository(dbContext context) : base(context)
    {
    }

    public async Task<User> GetUser(string ID)
    {
      return await Get().FirstOrDefaultAsync<User>(s => s.Id == ID);

    }

    public async Task<User> AddUser(User user)
    {
      await InsertAsync(user);

      return await Get().FirstOrDefaultAsync<User>(s => s.Email == user.Email.ToString());

    }

    public async Task<User> GetUserByEmail(string email)
    {

        return await Get().FirstOrDefaultAsync<User>(s => s.Email.Trim().ToLower() == email.Trim().ToLower());


    }
  }
}

