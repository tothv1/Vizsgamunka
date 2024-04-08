namespace AuthAPI.DTOs
{
    public class UpdateUserStatsDTO
    {
        public string Userid { get; set; } = null!;

        public int Kills { get; set; }

        public int Deaths { get; set; }

        public int HighestKillCount { get; set; }

        public int HighestLevel { get; set; }

        public int Timesplayed { get; set; }
    }
}
