using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class Userachievement
{
    public int UserId { get; set; }

    public int AchievementId { get; set; }

    public int AchiConnectId { get; set; }

    public DateTime AchievementDate { get; set; }

    public virtual AchievementsConnect AchiConnect { get; set; } = null!;
    public virtual Achievement Achievement { get; set; } = null!;
}
