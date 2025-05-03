namespace TheClassMain.Pages
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Model;
    using TheClassMain.Query;

    public partial class Register : Window
    {
        private ObservableCollection<Customer> customersList = new ObservableCollection<Customer>();
        public Register()
        {
            InitializeComponent();
        }

        public ObservableCollection<Customer> CustomerList => customersList;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCustomer();
        }

        private async Task LoadCustomer()
        {
            using (var context = new TableContext())
            {
                var users = await context.CustomersT.ToListAsync();
                customersList.Clear();
                foreach (var user in users)
                    customersList.Add(user);
            }
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();

            this.Close();
        }
        private void Login_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();

            }
        }
        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserName.Text) || string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new TableContext())
            {
                bool usernameExists = await context.CustomersT.AnyAsync(c => c.UserName == UserName.Text);
                bool emailExists = await context.CustomersT.AnyAsync(c => c.Email == Email.Text);

                if (usernameExists)
                {
                    MessageBox.Show("Ce nom d'utilisateur est déjà utilisé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (emailExists)
                {
                    MessageBox.Show("Cet email est déjà utilisé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Customer newCustomer = new Customer()
                {
                    UserName = UserName.Text,
                    Email = Email.Text,
                    Pwd = Password.Text
                };

                context.CustomersT.Add(newCustomer);
                await context.SaveChangesAsync();
            }

            UserName.Clear();
            Email.Clear();
            Password.Clear();
            await LoadCustomer();
        }
    }
}
