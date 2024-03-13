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
    /// Interaction logic for Teljesitmenyek.xaml
    /// </summary>
    public partial class Teljesitmenyek : Page
    {

        public static Teljesitmenyek instance = new Teljesitmenyek();

        public Teljesitmenyek()
        {
            InitializeComponent();

            DG_Teljesitmenyek.ItemsSource = MainPage.Teljesitmenyek;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
