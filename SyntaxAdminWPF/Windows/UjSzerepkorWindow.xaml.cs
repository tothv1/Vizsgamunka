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
    /// Interaction logic for UjSzerepkorPage.xaml
    /// </summary>
    public partial class UjSzerepkorWindow : Window
    {
        public string API_PATH = "https://localhost:7096";
        public UjSzerepkorWindow()
        {
            InitializeComponent();

            ResizeMode = ResizeMode.NoResize;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");

                Role role = new Role();
                role.roleid = 0;
                role.roleName = TB_Rolename.Text;

                if(role.roleName.Length == 0)
                {
                    MessageBox.Show("Adj meg egy nevet!");
                    return;
                }

                StringContent stringContentRole = new(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
                HttpResponseMessage responseRole = client.PostAsync(API_PATH + "/Role/createRole", stringContentRole).Result;
                string responseRoleBody = responseRole.Content.ReadAsStringAsync().Result;
                dynamic responseBody = JsonConvert.DeserializeObject<dynamic>(responseRoleBody)!;
                string responseMessage = responseBody.responseMessage;
                MessageBox.Show(responseMessage);

                //Adatok frissítése a datagridben is
                AdminPanel.instance.GenerateRoleData();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
