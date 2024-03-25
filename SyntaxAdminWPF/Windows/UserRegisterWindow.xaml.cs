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
using System.Windows.Shapes;

namespace SyntaxAdminWPF.Windows
{
    /// <summary>
    /// Interaction logic for UserRegister.xaml
    /// </summary>
    public partial class UserRegisterWindow : Window
    {
        public string API_PATH = "https://localhost:7096";

        public UserRegisterWindow()
        {
            string randomPass = generateNewPassword(16);

            InitializeComponent();
            TB_PasswordReg.IsEnabled = false;
            TB_PasswordRepeatReg.IsEnabled = false;
            TB_PasswordReg.Password = randomPass;
            TB_PasswordRepeatReg.Password = randomPass;

            ResizeMode = ResizeMode.NoResize;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();

                RegisterDTO temp = new RegisterDTO
                {

                    Username = TB_UsernameReg.Text,
                    Fullname = TB_FullnameReg.Text,
                    Email = TB_EmailReg.Text,
                    Password = TB_PasswordReg.Password,
                    PasswordRepeate = TB_PasswordRepeatReg.Password,

                };
                StringContent stringContent = new(JsonConvert.SerializeObject(temp), Encoding.UTF8, "application/json");
                HttpResponseMessage respone = client.PostAsync(API_PATH + "/Auth/Register", stringContent).Result;
                string responseBody = respone.Content.ReadAsStringAsync().Result;
                dynamic responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody)!;
                string responseMessage = responseObject["responseMessage"];
                MessageBox.Show(responseMessage);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        public string generateNewPassword(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] symbols = "<>#&@{};>.:-_'+!%/=()$÷".ToCharArray();
            char[] numbers = "0123456789".ToCharArray();

            List<char[]> arrrays =
            [
                alpha,
                symbols,
                numbers
            ];

            for (int i = 0; i < length; i++)
            {
                char[] selectedAray = arrrays[random.Next(arrrays.Count)];
                sb.Append(selectedAray[random.Next(selectedAray.Length)]);
            }
            return sb.ToString();
        }
    }
}
