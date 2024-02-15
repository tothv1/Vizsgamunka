using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class ConfirmationKey
{
    public int Keyid { get; set; }

    public string Userid { get; set; } = null!;

    public string ConfirmationKey1 { get; set; } = null!;

    public int ExpirationTime { get; set; }

    public virtual ICollection<RegisteredUser> RegisteredUsers { get; set; } = new List<RegisteredUser>();
}
