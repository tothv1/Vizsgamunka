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
    /// Interaction logic for Felhasznalok.xaml
    /// </summary>
    public partial class Felhasznalok : Page
    {
        public static Felhasznalok instance = new Felhasznalok();

        public Felhasznalok()
        {
            InitializeComponent();

            FillFelhasznaloLista();

            DG_Felhasznalok.IsReadOnly = true;
        }

        private void FillFelhasznaloLista()
        {
            DG_Felhasznalok.ItemsSource = MainPage.FelhasznaloLista;
            DG_Felhasznalok.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DG_Felhasznalok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UserUpdate updateWindow = new UserUpdate();
                User selectedUser = (User)DG_Felhasznalok.SelectedItem;
                UserUpdate.selectedItem = selectedUser;

                if (updateWindow != null && selectedUser != null)
                {
                    updateWindow.TB_Email.Text = selectedUser.Email;
                    updateWindow.TB_Fullname.Text = selectedUser.FullName;
                    updateWindow.TB_Username.Text = selectedUser.Username;
                    updateWindow.TB_Regdate.Text = selectedUser.RegDate.ToString();
                    updateWindow.TB_Isloggedin.Text = selectedUser.IsLoggedIn.ToString();
                    updateWindow.CB_Role.Text = selectedUser.UserRole.ToString();
                    updateWindow.TB_Kills.Text = selectedUser.Kills.ToString();
                    updateWindow.TB_HighestKills.Text = selectedUser.HighestKills.ToString();
                    updateWindow.TB_HighestLevel.Text = selectedUser.HighestLevel.ToString();
                    updateWindow.TB_Deaths.Text = selectedUser.Deaths.ToString();
                    updateWindow.TB_TimesPlayed.Text = selectedUser.TimesPlayed.ToString();

                    updateWindow.ShowDialog();
                }
                DG_Felhasznalok.UnselectAllCells();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
