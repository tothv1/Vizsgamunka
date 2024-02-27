using System;
using System.Collections.Generic;

namespace SyntaxBackEnd.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Regdate { get; set; }

    public DateTime Lastlogin { get; set; }

    public int Roleid { get; set; }

    public int UserStatsId { get; set; }

    public virtual ICollection<AchievementsConnect> AchievementsConnects { get; set; } = new List<AchievementsConnect>();

    public virtual Role Role { get; set; } = null!;

    public virtual Userstat UserStats { get; set; } = null!;
}
