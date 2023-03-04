using System;
namespace DataAccess.Interfaces
{
  public interface IEncryptService
  {
     string ComputeHash(string plainText);

  }
}

