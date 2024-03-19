using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAdminWPF.Models
{
    public class UserStats
    {
        public int userStatId {  get; set; }
        public int Kills { get; set; }
        public int highestKillCount { get; set; }
        public int highestLevel { get; set; }
        public int Deaths { get; set; }
        public int TimesPlayed { get; set; }
    }
}
