using System;
using System.Collections.Generic;
using Mercury.Shared.Models.Mercury;

namespace Mercury.Shared.Models.AspNetUser;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    [Visible]
    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    [Visible]
    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    [Visible]
    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    [Visible]
    public string? PhoneNumber { get; set; }

    [Visible]
    public bool PhoneNumberConfirmed { get; set; }

    [Visible]
    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    [Visible]
    public int AccessFailedCount { get; set; }

    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
    public virtual ICollection<Person> Persons { get; set; } = new List<Person>();
}
