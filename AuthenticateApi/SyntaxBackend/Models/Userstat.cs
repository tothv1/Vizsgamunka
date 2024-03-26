using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SyntaxBackEnd.Models;

public partial class Userstat
{
    public int UserStatId { get; set; }

    public int Kills { get; set; }
    
    public int HighestKillCount { get; set; }
    
    public int Deaths { get; set; }

    public int HighestLevel { get; set; }

    public int Timesplayed { get; set; }

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();


    public bool isValidName(string type)
    {
        return getByKey(type) != 0;
    }

    public int getByKey(string type)
    {
        switch (type)
        {
            case "kills":
                return Kills;
            case "deaths":
                return Deaths;
            case "highestkillcount":
                return HighestKillCount;
            case "highestlevel":
                return HighestLevel;
            case "timesplayed":
                return Timesplayed;
            default:
                return 0;
        }
    }
}
