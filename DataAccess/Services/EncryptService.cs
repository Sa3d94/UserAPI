using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DataAccess.Interfaces;

namespace DataAccess.Services
{
  public  class EncryptService : IEncryptService
  {
    public string ComputeHash(string plainText)
    {

      // Convert plain text into a byte array.
      byte[] plainTextBytes = Encoding.UTF8.GetBytes(String.Concat(plainText , "450d0b0db2bcf4adde5032eca1a7c416e560cf44"));

      HashAlgorithm hash = new SHA1Managed(); ;

      // Compute hash value of our plain text with appended salt.
      byte[] hashBytes = hash.ComputeHash(plainTextBytes);


      // Convert result into a base64-encoded string.
      string hashValue = Convert.ToBase64String(hashBytes);

      // Return the result.
      return RemoveSpecialCharacters(hashValue);
    }

    private static string RemoveSpecialCharacters(string text)
    {
      return Regex.Replace(text, "[^0-9A-Za-z _-]", "");
    }
  }
}

