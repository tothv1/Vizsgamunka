using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class Userachievement
{
    public int AchievementId { get; set; }

    public int UserId { get; set; }

    public DateTime AchievementDate { get; set; }

    public virtual Achievement Achievement { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;
}
