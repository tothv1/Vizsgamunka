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

    public int UserStatsId { get; set; }

    public virtual ICollection<AchievementsConnect> AchievementsConnects { get; set; } = new List<AchievementsConnect>();

    public virtual Permission Permission { get; set; } = null!;

    public virtual Userstat UserStats { get; set; } = null!;
}
