using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public partial class Achievement
{
    public int AchievementId { get; set; }

    public string AchievementName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<UserAchievement> UserAchievements { get; set; } = new List<UserAchievement>();
}
