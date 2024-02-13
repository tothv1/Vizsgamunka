using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int Roleid { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual RegisteredUser UserRoleNavigation { get; set; } = null!;
}
