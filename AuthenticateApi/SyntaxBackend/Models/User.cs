using System;
using System.Collections.Generic;

namespace SyntaxBackEnd.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Regdate { get; set; }

    public DateTime Lastlogin { get; set; }

    public int PermissionId { get; set; }

    public int UserAchievementsId { get; set; }

    public int UserStatsId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Userachievement UserAchievements { get; set; } = null!;

    public virtual Userstat UserStats { get; set; } = null!;
}
