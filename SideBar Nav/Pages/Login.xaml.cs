namespace TheClassMain.Pages
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using TheClassMain.Model;
    using TheClassMain.Query;
    using TheClassMain.Service;

    public partial class Login : Window
    {
        private ObservableCollection<Customer> customersList = new ObservableCollection<Customer>();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

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
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var register = new Register();
            register.Show();

            this.Close();
        }
    }
}
