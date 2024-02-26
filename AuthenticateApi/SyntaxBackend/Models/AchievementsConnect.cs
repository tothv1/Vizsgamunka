using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class AchievementsConnect
{
    public int AchiId { get; set; }

    public int Userid { get; set; }

    [JsonIgnore]
    public virtual User User { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Userachievement> Userachievements { get; set; } = new List<Userachievement>();
}
