using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class UserAchievement
{
    public int AchievementId { get; set; }

    public string Userid { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<UserAchievementDetail> UserAchievementDetails { get; set; } = new List<UserAchievementDetail>();
}
