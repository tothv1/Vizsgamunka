using Newtonsoft.Json;
using SyntaxAdminWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyntaxAdminWPF.Pages
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    /// 

    public partial class AdminPanel : Page
    {
        private string AUTH_API_PATH = "https://localhost:7096";
        private string GAME_API_PATH = "https://localhost:7275";

        public static AdminPanel instance { get; private set; } = new AdminPanel();

        public AdminPanel()
        {
            InitializeComponent();

            LB_loggedInText.Content = ($"Bejelentkezett felhasználó, {MainPage.ResponseTokenData["username"]}");


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.ResponseToken = null!;
            MainPage.instance.ResizeMode = ResizeMode.NoResize;
            MainPage.instance.Height = 450;
            MainPage.instance.Width = 800;

            MainPage.instance.UpdateLayout();

            NavigationService.GoBack();

        }

        private void Button_NavigateToUsers(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainPage.ResponseToken != null)
                {
                    NavigationService.Navigate(new Uri(".\\Pages\\Felhasznalok.xaml", UriKind.RelativeOrAbsolute));

                    GenerateData();
                }
                else
                {
                    MessageBox.Show("Sikertelen adatlekérés! Jelentkezz be újra!");
                    NavigationService.Navigate(new Uri(".\\Pages\\Login.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Váratlan hiba: " + ex.Message);
            }

        }

        public void GenerateData()
        {
            try
            {
                HttpResponseMessage resultAuth = GetFromAuth("/User/users");
                string responseAuthBody = resultAuth.Content.ReadAsStringAsync().Result;

                HttpResponseMessage resultGame = GetFromGame("/Game/getUsers");
                string responseGameBody = resultGame.Content.ReadAsStringAsync().Result;

                HttpResponseMessage resultRoles = GetFromAuth("/Role/Roles");
                string responseRoleBody = resultRoles.Content.ReadAsStringAsync().Result;

                dynamic auth_users = JsonConvert.DeserializeObject(responseAuthBody)!;
                dynamic game_users = JsonConvert.DeserializeObject(responseGameBody)!;
                dynamic authroles = JsonConvert.DeserializeObject(responseRoleBody)!;

                MainPage.FelhasznaloLista.Clear();
                MainPage.FelhasznaloRoleok.Clear();

                foreach (var role in authroles)
                {
                    Role temp = new Role();
                    temp.Id = role.roleid;
                    temp.RoleName = role.roleName;
                    MainPage.FelhasznaloRoleok.Add(temp);
                }

                foreach (var authUser in auth_users)
                {
                    User temp = new User();
                    temp.Id = authUser.userid;
                    temp.Username = authUser.username;
                    temp.FullName = authUser.fullname;
                    temp.Email = authUser.email;
                    temp.RegDate = authUser.regdate;
                    temp.LastLogin = DateTime.Now;
                    int roleId = authUser["roleid"];
                    temp.UserRole = MainPage.FelhasznaloRoleok.FirstOrDefault(r => r.Id == roleId)!.RoleName;
                    temp.IsLoggedIn = authUser.isLoggedIn;
                    temp.UserStatsId = 0;
                    temp.Kills = 0;
                    temp.Deaths = 0;
                    temp.TimesPlayed = 0;
                    MainPage.FelhasznaloLista.Add(temp);
                    //MessageBox.Show(temp.ToString() + "");
                }

                foreach (var gameUser in game_users)
                {
                    foreach (var authUser in MainPage.FelhasznaloLista)
                    {
                        var userStats = gameUser.userStats;
                        if (gameUser.id == authUser.Id)
                        {
                            authUser.Kills = userStats.kills;
                            authUser.Deaths = userStats.deaths;
                            authUser.TimesPlayed = userStats.timesplayed;
                            authUser.LastLogin = gameUser.lastlogin;
                            authUser.UserStatsId = gameUser.userStatsId;
                            //MessageBox.Show(gameUser + "");
                        }
                    }
                }
                Felhasznalok.instance.DG_Felhasznalok.ItemsSource = MainPage.FelhasznaloLista;
                CollectionViewSource.GetDefaultView(Felhasznalok.instance.DG_Felhasznalok.ItemsSource).Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonAchievements(object sender, RoutedEventArgs e)
        {

            try
            {
                if (MainPage.ResponseToken != null)
                {
                    NavigationService.Navigate(new Uri(".\\Pages\\Teljesitmenyek.xaml", UriKind.RelativeOrAbsolute));

                    GenerateAchiData();
                }
                else
                {
                    MessageBox.Show("Sikertelen adatlekérés! Jelentkezz be újra!");
                    NavigationService.Navigate(new Uri(".\\Pages\\Login.xaml", UriKind.RelativeOrAbsolute));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Váratlan hiba: " + ex.Message);
            }
        }

        private void GenerateAchiData()
        {
            //MainPage.Teljesitmenyek.Clear();

            HttpResponseMessage resultGame = GetFromGame("/Game/achievements");
            string responseGameAchievement = resultGame.Content.ReadAsStringAsync().Result;

            dynamic achievements = JsonConvert.DeserializeObject(responseGameAchievement)!;

            foreach (var achi in achievements)
            {
                MainPage.Teljesitmenyek.Add(new Achievement
                {
                    Id = achi.id,
                    AchievementName = achi.achievementName,
                });
            }
            Teljesitmenyek.instance.DG_Teljesitmenyek.ItemsSource = MainPage.Teljesitmenyek;
            CollectionViewSource.GetDefaultView(Teljesitmenyek.instance.DG_Teljesitmenyek.ItemsSource).Refresh();

        }

        private void ButtonRoles(object sender, RoutedEventArgs e)
        {

        }


        private HttpResponseMessage GetFromAuth(string endpoint)
        {
            string auth_url = AUTH_API_PATH + endpoint;
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");
            var request = client.GetAsync(auth_url);

            return request.Result;
        }

        private HttpResponseMessage GetFromGame(string endpoint)
        {
            string game_url = GAME_API_PATH + endpoint;
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");
            var request = client.GetAsync(game_url);

            return request.Result;
        }

    }
}
