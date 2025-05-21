using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Query;
using TheClassMain.Command;
using TheClassMain.Views;

namespace TheClassMain.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        private ObservableCollection<Customer> customersList = new ObservableCollection<Customer>();
        private string userName;
        private string email;
        private string password;

        public RegisterViewModel()
        {
            RegisterCommand = new RelayCommand(async _ => await RegisterCustomer());
            LoginCommand = new RelayCommand(_ => OpenLogin());
        }

        public ObservableCollection<Customer> CustomersList => customersList;

        public string UserName
        {
            get => userName;
            set { userName = value; OnPropertyChanged(); }
        }

        public string Email
        {
            get => email;
            set { email = value; OnPropertyChanged(); }
        }

        public string Password
        {
            get => password;
            set { password = value; OnPropertyChanged(); }
        }

        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }

        public async Task LoadCustomer()
        {
            using (var context = new TableContext())
            {
                var users = await context.CustomersT.ToListAsync();
                customersList.Clear();
                foreach (var user in users)
                    customersList.Add(user);
            }
        }

        private async Task RegisterCustomer()
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new TableContext())
            {
                if (await context.CustomersT.AnyAsync(c => c.UserName == UserName))
                {
                    MessageBox.Show("Ce nom d'utilisateur est déjà utilisé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (await context.CustomersT.AnyAsync(c => c.Email == Email))
                {
                    MessageBox.Show("Cet email est déjà utilisé.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newCustomer = new Customer
                {
                    UserName = UserName,
                    Email = Email,
                    Pwd = Password
                };
                MessageBox.Show("Votre Compte à été créer - appuez sur le bouton LOGIN pour vous connectez", "Compte Créer" ,MessageBoxButton.OK, MessageBoxImage.Information);
                context.CustomersT.Add(newCustomer);
                await context.SaveChangesAsync();
            }

            UserName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            await LoadCustomer();
        }

        private void OpenLogin()
        {
            var loginWindow = new Login();
            loginWindow.Show();
            Application.Current.Windows[0]?.Close();
        }
    }
}
