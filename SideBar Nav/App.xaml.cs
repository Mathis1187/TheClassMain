namespace TheClassMain
{
    using System.Windows;
    using TheClassMain.Pages;
    using TheClassMain.Query;

    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            InitializeComponent();
            var login = new Login();
            login.Show();
            using (var context = new TableContext())
            {
                context.CreateDatabase();
            }
        }
    }
}
