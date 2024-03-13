using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxAdminWPF.Models
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsLoggedIn { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime LastLogin { get; set; }
        public string UserRole { get; set; } = null!;
        public int Kills { get; set; }
        public int Deaths { get; set; }
        public int TimesPlayed { get; set; }

        public override string ToString()
        {
            return $"Id: {Id},\n FullName: {FullName},\n Username: {Username},\n Email: {Email},\n IsLoggedIn: {IsLoggedIn},\n RegDate: {RegDate},\n LastLogin: {LastLogin},\n UserRole: {UserRole},\n Kills: {Kills},\n Deaths: {Deaths},\n TimesPlayed: {TimesPlayed}";
        }
    }
}
