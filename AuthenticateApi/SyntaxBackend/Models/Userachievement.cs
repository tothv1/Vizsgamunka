using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class Userachievement
{
    public int UserId { get; set; }

    public int AchievementId { get; set; }

    public DateTime AchievementDate { get; set; }

    [JsonIgnore]
    public virtual Achievement Achievement { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
