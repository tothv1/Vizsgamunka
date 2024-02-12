using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class Userstat
{
    public int UserId { get; set; }

    public int Kills { get; set; }

    public int Deaths { get; set; }

    public int Timesplayed { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
