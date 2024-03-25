using Newtonsoft.Json;
using SyntaxAdminWPF.Models;
using SyntaxAdminWPF.Pages;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for UjTeljesitmenyWindow.xaml
    /// </summary>
    public partial class UjTeljesitmenyWindow : Window
    {
        public string API_PATH = "https://localhost:7096";
        public UjTeljesitmenyWindow()
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

                Achievement achi = new Achievement();
                achi.achievementName = TB_AchievementName.Text;

                if (achi.achievementName.Length == 0)
                {
                    MessageBox.Show("Adj meg egy nevet!");
                    return;
                }


                StringContent stringContentAchi = new(JsonConvert.SerializeObject(achi), Encoding.UTF8, "application/json");
                HttpResponseMessage responseAchi = client.PostAsync(API_PATH + "/Game/createAchievement", stringContentAchi).Result;
                string responseAchiBody = responseAchi.Content.ReadAsStringAsync().Result;
                MessageBox.Show(responseAchiBody);

                //Adatok frissítése a datagridben is
                AdminPanel.instance.GenerateAchiData();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
