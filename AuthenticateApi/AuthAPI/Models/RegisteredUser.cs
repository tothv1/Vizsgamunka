using System;
using System.Collections.Generic;

namespace AuthAPI.Models;

public partial class RegisteredUser
{
    public string Userid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Hash { get; set; } = null!;

    public bool IsLoggedIn { get; set; }

    public DateTime Regdate { get; set; }

    public int Roleid { get; set; }

    public string? ChangePasswordConfirmationKey { get; set; }

    public virtual Role Role { get; set; } = null!;
}
