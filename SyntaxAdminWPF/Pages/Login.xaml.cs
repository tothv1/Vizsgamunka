using Newtonsoft.Json;
using SyntaxAdminWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
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
using static System.Net.Mime.MediaTypeNames;

namespace SyntaxAdminWPF.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {

        public string API_PATH = "https://localhost:7096/Auth";

        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = TB_username.Text;
            var password = TB_password.Password;

            string url = API_PATH + "/login";

            HttpClient client = new();

            LoginDTO userCredentials = new(username, password);

            StringContent stringContent = new(JsonConvert.SerializeObject(userCredentials), Encoding.UTF8, "application/json");


            try
            {
                HttpResponseMessage result = client.PostAsync(url, stringContent).Result;

                string responseBody = result.Content.ReadAsStringAsync().Result;
                
                dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody)!;

                if (responseObject["status"] == 200)
                {
                    string token = responseObject["resObj"];
                    string tokenTemp = JsonConvert.SerializeObject(MainPage.JwtDecode(token));
                    dynamic tokenPayload = JsonConvert.DeserializeObject(tokenTemp)!;
                    dynamic tokenObject = tokenPayload["Payload"];
                    MainPage.ResponseToken = tokenObject;

                    if (tokenObject["role"] == "Admin")
                    {
                        NavigationService.Navigate(new Uri(".\\Pages\\AdminPanel.xaml", UriKind.RelativeOrAbsolute));
                        MainPage.instance.ResizeMode = ResizeMode.CanResize; 
                        return;
                    }
                    MessageBox.Show("Nem vagy admin");
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
