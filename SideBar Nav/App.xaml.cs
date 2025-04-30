using System.Windows;
using TheClassMain.Pages;

namespace TheClassMain
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var compte = new Compte();
            compte.Show();
        }
    }
}
