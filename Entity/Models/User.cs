using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public bool MarketingConsent { get; set; }
}
