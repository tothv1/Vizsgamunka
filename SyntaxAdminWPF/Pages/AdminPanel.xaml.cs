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


                    try
                    {
                        HttpResponseMessage resultAuth = GetFromAuth("/User/users");
                        string responseAuthBody = resultAuth.Content.ReadAsStringAsync().Result;

                        HttpResponseMessage resultGame = GetFromGame("/Game/getUsers");
                        string responseGameBody = resultGame.Content.ReadAsStringAsync().Result;

                        dynamic auth_users = JsonConvert.DeserializeObject(responseAuthBody)!;
                        dynamic game_users = JsonConvert.DeserializeObject(responseGameBody)!;

                        foreach (var authUser in auth_users)
                        {
                            
                            User temp = new User();
                            temp.Id = authUser.userid;
                            temp.Username = authUser.username;
                            temp.FullName = authUser.fullname;
                            temp.Email = authUser.email;
                            temp.RegDate = authUser.regdate;
                            temp.LastLogin = DateTime.Now;
                            temp.RoleId = authUser.roleid;
                            temp.UserStatsId = 0;
                            MainPage.FelhasznaloLista.Add(temp); 
                        }
                        foreach (var gameUser in game_users)
                        {
                           
                            var user = gameUser;
                            foreach (var authUser in MainPage.FelhasznaloLista)
                            {
                                if(user.id == authUser.Id)
                                {
                                    authUser.UserStatsId = user.userStatsId;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
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

        private void ButtonAchievements(object sender, RoutedEventArgs e)
        {

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
