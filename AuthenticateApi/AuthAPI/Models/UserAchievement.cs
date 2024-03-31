using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public partial class UserAchievement
{
    public int UserAchievementId { get; set; }

    public string Userid { get; set; } = null!;

    public int AchievementId { get; set; }

    public DateTime AchievementDate { get; set; }

    public virtual Achievement Achievement { get; set; } = null!;

    [JsonIgnore]
    public virtual RegisteredUser User { get; set; } = null!;
}
