﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class User
{

    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime Regdate { get; set; }

    public DateTime Lastlogin { get; set; }

    [JsonIgnore]
    public int PermissionId { get; set; }

    [JsonIgnore]
    public int UserAchievementsId { get; set; }

    [JsonIgnore]
    public int UserStatsId { get; set; }

    public virtual Permission Permission { get; set; } = null!;

    public virtual Userstat UserStats { get; set; } = null!;

    public virtual ICollection<Userachievement> Userachievements { get; set; } = new List<Userachievement>();


}
