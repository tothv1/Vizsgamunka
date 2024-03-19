using SyntaxAdminWPF.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SyntaxAdminWPF.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {

        public static MainPage instance = new MainPage();
        public static dynamic ResponseTokenData { get; set; } = null!;
        public static string ResponseToken { get; set; } = null!;

        public static List<User> FelhasznaloLista = new List<User>();

        public static List<Role> FelhasznaloRoleok = new List<Role>();

        public MainPage()
        {
            InitializeComponent();
        }

        public static JwtSecurityToken JwtDecode(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            return jwtHandler.ReadJwtToken(token);
        }
    }
}
