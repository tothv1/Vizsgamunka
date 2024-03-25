using Newtonsoft.Json;
using SyntaxAdminWPF.Models;
using SyntaxAdminWPF.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SyntaxAdminWPF.Pages
{
    /// <summary>
    /// Interaction logic for Szerepkorok.xaml
    /// </summary>
    public partial class Szerepkorok : Page
    {
        public static Szerepkorok instance = new Szerepkorok();

        public Szerepkorok()
        {
            InitializeComponent();

            DG_Szerepkorok.ItemsSource = MainPage.FelhasznaloRoleok;

            DG_Szerepkorok.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DG_Szerepkorok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SzerepkorUpdateWindow updateWindow = new SzerepkorUpdateWindow();
                Role selectedRole = (Role)DG_Szerepkorok.SelectedItem;
                SzerepkorUpdateWindow.selectedItem = selectedRole;

                if (updateWindow != null && selectedRole != null)
                {
                    updateWindow.TB_Rolename.Text = selectedRole.roleName;
                    updateWindow.ShowDialog();
                }
                DG_Szerepkorok.UnselectAllCells();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           UjSzerepkorWindow ujSzerepkorWindow = new UjSzerepkorWindow();

            ujSzerepkorWindow.ShowDialog();
        }
    }
}
