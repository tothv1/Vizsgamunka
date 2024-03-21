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
    /// Interaction logic for SzerepkorUpdateWindow.xaml
    /// </summary>
    public partial class SzerepkorUpdateWindow : Window
    {

        public static object selectedItem = null!;
        public string API_PATH = "https://localhost:7096";

        public SzerepkorUpdateWindow()
        {
            InitializeComponent();

            ResizeMode = ResizeMode.NoResize;
        }
        private void RoleUpdateSave(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedItem != null)
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");

                    Role selectedRole = (Role)selectedItem;

                    Role role = new Role();
                    role.roleid = selectedRole.roleid;
                    role.roleName = TB_Rolename.Text;

                    StringContent stringContentRole = new(JsonConvert.SerializeObject(role), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseRole = client.PutAsync(API_PATH + "/Role/updateRole", stringContentRole).Result;
                    string responseRoleBody = responseRole.Content.ReadAsStringAsync().Result;

                    MessageBox.Show(responseRoleBody);
                }
                //Adatok frissítése a datagridben is
                AdminPanel.instance.GenerateRoleData();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DeleteRoleButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedItem != null)
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");

                    Role selectedRole = (Role)selectedItem;

                    HttpResponseMessage responseAchi = client.DeleteAsync(API_PATH + $"/Role/deleteRole?roleid={selectedRole.roleid}").Result;
                    string responseRoleBody = responseAchi.Content.ReadAsStringAsync().Result;

                    MessageBox.Show(responseRoleBody);
                }
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
