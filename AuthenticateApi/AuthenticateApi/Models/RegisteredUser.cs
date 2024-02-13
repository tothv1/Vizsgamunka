using System;
using System.Collections.Generic;

namespace AuthenticateApi.Models;

public partial class RegisteredUser
{
    public string Userid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public DateOnly Regdate { get; set; }

    public int? Roleid { get; set; }

    public virtual UserRole? UserRole { get; set; }
}
