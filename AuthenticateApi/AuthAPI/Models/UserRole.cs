using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int Roleid { get; set; }

    public virtual ICollection<RegisteredUser> RegisteredUsers { get; set; } = new List<RegisteredUser>();

    public virtual Role Role { get; set; } = null!;
}
