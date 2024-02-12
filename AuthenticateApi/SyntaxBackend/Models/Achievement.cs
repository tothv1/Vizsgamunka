using System;
using System.Collections.Generic;

namespace SyntaxBackEnd.Models;

public partial class Achievement
{
    public int Id { get; set; }

    public string AchievementName { get; set; } = null!;

    public virtual ICollection<Userachievement> Userachievements { get; set; } = new List<Userachievement>();
}
