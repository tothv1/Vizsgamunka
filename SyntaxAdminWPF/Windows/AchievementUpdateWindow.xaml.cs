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
    /// Interaction logic for AchievementUpdateWindow.xaml
    /// </summary>
    public partial class AchievementUpdateWindow : Window
    {
        public string API_PATH = "https://localhost:7096";
        public static object selectedItem = null!;

        public AchievementUpdateWindow()
        {
            InitializeComponent();

            ResizeMode = ResizeMode.NoResize;
        }

        private void AchievementUpdateSave(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedItem != null)
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");

                    Achievement selectedAchi = (Achievement)selectedItem;

                    Achievement achi = new Achievement();
                    achi.Id = selectedAchi.Id;
                    achi.achievementName = TB_AchievementName.Text;

                    StringContent stringContentAchi = new(JsonConvert.SerializeObject(achi), Encoding.UTF8, "application/json");
                    HttpResponseMessage responseAchi = client.PutAsync(API_PATH + "/Game/updateAchievement", stringContentAchi).Result;
                    string responseAchiBody = responseAchi.Content.ReadAsStringAsync().Result;
                    

                    MessageBox.Show(responseAchiBody);
                }
                //Adatok frissítése a datagridben is
                AdminPanel.instance.GenerateAchiData();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AchievementDeleteButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if (selectedItem != null)
                {
                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {MainPage.ResponseToken}");

                    Achievement selectedAchi = (Achievement)selectedItem;

                    HttpResponseMessage responseAchi = client.DeleteAsync(API_PATH + $"/Game/deleteAchievement?id={selectedAchi.Id}").Result;
                    string responseAchiBody = responseAchi.Content.ReadAsStringAsync().Result;

                    MessageBox.Show(responseAchiBody);
                }
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
