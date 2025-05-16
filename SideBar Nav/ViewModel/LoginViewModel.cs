using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Command;
using TheClassMain.Model;
using TheClassMain.Pages;
using TheClassMain.Query;
using TheClassMain.Service;

namespace TheClassMain.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string userName;
        public string UserName
        {
            get => userName;
            set { userName = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(async param => await LoginAsync(param));
            RegisterCommand = new RelayCommand(_ => OpenRegister());
        }

        private async Task LoginAsync(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox?.Password;

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Veuillez entrer un nom d'utilisateur et un mot de passe.");
                return;
            }

            using (var context = new TableContext())
            {
                var customer = await context.CustomersT
                    .FirstOrDefaultAsync(c => c.UserName == UserName && c.Pwd == password);

                if (customer != null)
                {
                    Session.CurrentCustomer = customer;

                    var mainWindow = new MainWindow();
                    mainWindow.Show();

                    CloseLoginWindow();
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur et mot de passe invalide.");
                }
            }
        }

        private void OpenRegister()
        {
            var register = new Register();
            register.Show();
            CloseLoginWindow();
        }

        private void CloseLoginWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window is Pages.Login)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
