using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

    public DateTime? Lastlogin { get; set; }

    public int Roleid { get; set; }

    public string? ChangePasswordConfirmationKey { get; set; }

    [JsonIgnore]
    public virtual ICollection<LoggedInUser> LoggedInUsers { get; set; } = new List<LoggedInUser>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();

    public virtual UserStat? UserStat { get; set; }
}
