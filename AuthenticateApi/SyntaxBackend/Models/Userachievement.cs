using System;
using System.Collections.Generic;

namespace SyntaxBackEnd.Models;

public partial class Userachievement
{
    public string UserId { get; set; } = null!;

    public int AchievementId { get; set; }

    public int AchiConnectId { get; set; }

    public DateTime AchievementDate { get; set; }

    public virtual AchievementsConnect AchiConnect { get; set; } = null!;

    public virtual Achievement Achievement { get; set; } = null!;
}
