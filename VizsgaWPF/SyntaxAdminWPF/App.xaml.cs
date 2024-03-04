using SyntaxAdminWPF.Pages;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SyntaxAdminWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainPage mainPage = new MainPage();

            mainPage.ResizeMode = ResizeMode.NoResize;

            mainPage.Show();

        }
    }

}
