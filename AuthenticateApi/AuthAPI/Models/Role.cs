using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class Role
{
    public int Roleid { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<RegisteredUser> RegisteredUsers { get; set; } = new List<RegisteredUser>();
}
