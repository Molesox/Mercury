using System;
using System.Collections.Generic;

namespace Mercury.Shared.Models.AspNetUser;

public partial class AspNetRole
{
    public string Id { get; set; } = null!;

    [Visible]
    public string? Name { get; set; }

    [Visible]
    public string? NormalizedName { get; set; }

    [Visible]
    public string? ConcurrencyStamp { get; set; }

    public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; } = new List<AspNetRoleClaim>();

    public virtual ICollection<AspNetUser> Users { get; set; } = new List<AspNetUser>();
}
