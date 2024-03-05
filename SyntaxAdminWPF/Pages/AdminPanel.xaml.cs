using System;
using System.Collections.Generic;
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

        public static AdminPanel instance { get; private set; } = null!;

        public AdminPanel()
        {
            InitializeComponent();

            instance = this;

            LB_loggedInText.Content = ($"Bejelentkezett felhasználó, {MainPage.ResponseToken["username"]}");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.ResponseToken = null!;
            NavigationService.GoBack();
        }
    }
}
