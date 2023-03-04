using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Dtos
{
  public record UserDto
  {
    public string Id { get; set; }

    [Required]
    public string FirstName { get; set; }


    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public bool MarketingConsent { get; set; }
  }

  public record UserNoConsent
  {
    public string Id { get; set; }

    [Required]
    public string FirstName { get; set; }


    public string LastName { get; set; }


    [Required]
    public bool MarketingConsent { get; set; }
  }

  public record CreatedUser  {

    public string Id { get; set; }
    public string AccessToken { get; set; }
  }

}

