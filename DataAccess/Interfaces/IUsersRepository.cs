using System;
using Entity.Models;

namespace DataAccess.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
    Task<User> GetUser(string ID);
    Task<User> GetUserByEmail(string email);

    Task<User> AddUser(User user);



  }

}

