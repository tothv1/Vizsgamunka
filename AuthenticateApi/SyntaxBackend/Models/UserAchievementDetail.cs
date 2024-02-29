using System;
using System.Collections.Generic;

namespace SyntaxBackEnd.Models;

public partial class UserAchievementDetail
{
    public int AchievementDetailId { get; set; }

    public int AchievementId { get; set; }

    public int UserAchievementId { get; set; }

    public DateTime AchievementDate { get; set; }

    public virtual Achievement Achievement { get; set; } = null!;

    public virtual UserAchievement UserAchievement { get; set; } = null!;
}
