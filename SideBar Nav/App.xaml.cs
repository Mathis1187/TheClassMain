namespace TheClassMain
{
    using System.Windows;
    using TheClassMain.Pages;
    using TheClassMain.Query;

    /// <summary>
    /// Defines the <see cref="App" />
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The Application_Startup
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="StartupEventArgs"/></param>
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
