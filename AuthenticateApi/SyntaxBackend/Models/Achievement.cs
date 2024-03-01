using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class Achievement
{
    public int Id { get; set; }

    public string AchievementName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Userachievement> Userachievements { get; set; } = new List<Userachievement>();
}
