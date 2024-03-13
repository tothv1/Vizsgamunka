using Newtonsoft.Json;
using SyntaxAdminWPF.Models;
using SyntaxAdminWPF.Pages;
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
using System.Windows.Shapes;

namespace SyntaxAdminWPF.Windows
{
    /// <summary>
    /// Interaction logic for UserUpdate.xaml
    /// </summary>
    public partial class UserUpdate : Window
    {
        public string API_PATH = "https://localhost:7096";
        private string GAME_API_PATH = "https://localhost:7275";
        public static object selectedItem = null!;

        public UserUpdate()
        {
            InitializeComponent();

            ResizeMode = ResizeMode.NoResize;
        }

        //Szerkesztés mentése
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Felhasznalok.instance.DG_Felhasznalok.UnselectAllCells();
            try
            {
                if (selectedItem != null)
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");

                    User selectedUser = (User)selectedItem;

                    UpdateUserDTO updateUserDTO = new UpdateUserDTO();
                    updateUserDTO.username = TB_Username.Text;
                    updateUserDTO.roleid = MainPage.FelhasznaloRoleok.FirstOrDefault(r => r.RoleName == TB_Role.Text)!.Id;
                    updateUserDTO.fullname = TB_Fullname.Text;
                    updateUserDTO.email = TB_Email.Text;
                    updateUserDTO.regdate = DateTime.Parse(TB_Regdate.Text);
                    updateUserDTO.isLoggedIn = bool.Parse(TB_Isloggedin.Text);
                    updateUserDTO.userid = selectedUser.Id;

                    UserStats userStats = new UserStats();
                    userStats.userStatId = selectedUser.UserStatsId;
                    userStats.Kills = int.Parse(TB_Kills.Text);
                    userStats.Deaths = int.Parse(TB_Deaths.Text);
                    userStats.TimesPlayed = int.Parse(TB_TimesPlayed.Text);


                    StringContent stringContent = new(JsonConvert.SerializeObject(updateUserDTO), Encoding.UTF8, "application/json");
                    HttpResponseMessage respone = client.PutAsync(API_PATH + "/User/updateUser", stringContent).Result;
                    string responseBody = respone.Content.ReadAsStringAsync().Result;

                    StringContent stringContentStats = new(JsonConvert.SerializeObject(userStats), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseStats = client.PutAsync(API_PATH + "/Game/updateAccountStats", stringContentStats).Result;
                    string responseStatsBody = responseStats.Content.ReadAsStringAsync().Result;

                    MessageBox.Show(responseBody);
                } 
                
                //Adatok frissítése a datagridben is
                AdminPanel.instance.GenerateData();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Felhasználó törlése
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

           /* HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");

            LoginDTO unregisterObject = new LoginDTO();
            unregisterObject.Username = string.Empty;
            unregisterObject.Password = string.Empty;

            StringContent stringContentStats = new(JsonConvert.SerializeObject(unregisterObject), Encoding.UTF8, "application/json");
            HttpResponseMessage responseStats = client.PutAsync(API_PATH + "/Game/updateAccountStats", stringContentStats).Result;
            string responseStatsBody = responseStats.Content.ReadAsStringAsync().Result;*/

            MessageBox.Show("A felhasználót sikeresen tudod majd nemsokára törölni xd!");

            Close();
        }
    }
}
