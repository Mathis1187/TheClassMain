namespace TheClassMain.Pages
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using TheClassMain.Model;
    using TheClassMain.Query;
    using TheClassMain.Service;

    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Login : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class
        /// </summary>
        private ObservableCollection<Customer> customersList = new ObservableCollection<Customer>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The Login_OnMouseDown
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="MouseButtonEventArgs"/></param>
        private void Login_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// The Login_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new TableContext())
            {
                var userNameInput = UserName.Text;
                var userPdw = Password.Password;

                var customer = context.CustomersT
                    .FirstOrDefault(c => c.UserName == userNameInput && c.Pwd == userPdw);

                if (customer != null)
                {
                    Session.CurrentCustomer = customer;

                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur et mot de passe invalide.");
                }
            }
        }

        /// <summary>
        /// The Register_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var register = new Register();
            register.Show();

            this.Close();
        }
    }
}
