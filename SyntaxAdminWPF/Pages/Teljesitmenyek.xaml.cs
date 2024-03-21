using SyntaxAdminWPF.Models;
using SyntaxAdminWPF.Windows;
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

            DG_Teljesitmenyek.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DG_Teljesitmenyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AchievementUpdateWindow updateWindow = new AchievementUpdateWindow();
                Achievement selectedAchi = (Achievement)DG_Teljesitmenyek.SelectedItem;
                AchievementUpdateWindow.selectedItem = selectedAchi;

                if (updateWindow != null && selectedAchi != null)
                {
                    updateWindow.TB_AchievementName.Text = selectedAchi.achievementName;
                    updateWindow.ShowDialog();
                }
                DG_Teljesitmenyek.UnselectAllCells();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UjTeljesitmenyWindow ujTeljesitmenyWindow = new UjTeljesitmenyWindow();

            ujTeljesitmenyWindow.ShowDialog();
        }
    }
}
