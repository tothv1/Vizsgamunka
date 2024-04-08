using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json.Serialization;

namespace AuthAPI.Models;

public partial class UserStat
{
    public int UserStatId { get; set; }

    public string Userid { get; set; } = null!;

    public int Kills { get; set; }

    public int Deaths { get; set; }

    public int HighestKillCount { get; set; }

    public int HighestLevel { get; set; }

    public int Timesplayed { get; set; }

    [JsonIgnore]
    public virtual RegisteredUser User { get; set; } = null!;

    public bool IsValidName(string type)
    {
        PropertyInfo[] properties = typeof(UserStat).GetProperties();
        return properties.First(s => s.Name.ToLower() == type.ToLower()) != null;
    }

    public int GetByKey(string type)
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
