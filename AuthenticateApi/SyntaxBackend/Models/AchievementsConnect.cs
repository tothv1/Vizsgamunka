using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class AchievementsConnect
{
    public int AchiId { get; set; }

    public string Userid { get; set; } = null!;

    [JsonIgnore]
    public virtual User User { get; set; } = null!;

    public virtual ICollection<Userachievement> Userachievements { get; set; } = new List<Userachievement>();
}
