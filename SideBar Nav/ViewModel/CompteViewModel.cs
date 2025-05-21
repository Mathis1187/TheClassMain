using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Views;
using TheClassMain.Query;
using TheClassMain.Service;

namespace TheClassMain.ViewModel
{
    public class CompteViewModel : ViewModelBase
    {
        private string userUsername;
        private string userEmail;
        private string userPassword;
        private int userId;

        public string UserUsername
        {
            get => userUsername;
            set { userUsername = value; OnPropertyChanged(); }
        }

        public string UserEmail
        {
            get => userEmail;
            set { userEmail = value; OnPropertyChanged(); }
        }

        public string UserPassword
        {
            get => userPassword;
            set { userPassword = value; OnPropertyChanged(); }
        }

        public int UserId
        {
            get => userId;
            set { userId = value; OnPropertyChanged(); }
        }

        public CompteViewModel()
        {
            LoadUserInfo();
        }

        public async void LoadUserInfo()
        {
            var user = Session.CurrentCustomer;

            UserUsername = user.UserName;
            UserEmail = user.Email;
            UserPassword = user.Pwd;
            UserId = user.CustomerId;
        }
        
        
        public async Task UpdateCustomerUsername()
        {
            using (var context = new TableContext())
            {
                var customer = await context.CustomersT.FindAsync(UserId);

                if (customer != null)
                {
                    //MessageBox.Show("Username have been updated.");
                    string message = "Are you sure you want to update your username ?";
                    string title = "Update";
                    var res = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        customer.UserName = UserUsername;
                        context.CustomersT.Update(customer);
                        await context.SaveChangesAsync();
                        Session.CurrentCustomer.UserName = UserUsername;
                    }
                    else
                    {
                        UserUsername = Session.CurrentCustomer.UserName;
                    }
                }
                else
                {
                    MessageBox.Show("Retry later. The Database have some probem.");
                }
            }
        }            
        
        public async Task UpdateCustomerEmail()
        {
            using (var context = new TableContext())
            {
                var customer = await context.CustomersT.FindAsync(UserId);

                if (customer != null)
                {
                    string message = "Are you sure you want to update your Email ?";
                    string title = "Update";
                    var res = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        customer.Email = userEmail;
                        context.CustomersT.Update(customer);
                        await context.SaveChangesAsync();
                        Session.CurrentCustomer.Email = userEmail;
                    }
                    else
                    {
                        UserEmail = Session.CurrentCustomer.Email;
                    }
                }
                else
                {
                    MessageBox.Show("Retry later. The Database have some probem.");
                }
            }
        }            
        
        public async Task UpdateCustomerPwd()
        {
            using (var context = new TableContext())
            {
                var customer = await context.CustomersT.FindAsync(UserId);

                if (customer != null)
                {
                    string message = "Are you sure you want to update your Password ?";
                    string title = "Update";
                    var res = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        customer.Pwd = UserPassword;
                        context.CustomersT.Update(customer);
                        await context.SaveChangesAsync();
                        Session.CurrentCustomer.Pwd = UserPassword;
                    }
                    else
                    {
                        UserPassword = Session.CurrentCustomer.Pwd;
                    }
                }
                else
                {
                    MessageBox.Show("Retry later. The Database have some probem.");
                }
            }
        }    
        
        public async Task DeleteCustomer()
        {
            using (var context = new TableContext())
            {
                var customer = await context.CustomersT.FindAsync(UserId);
                var facture = await context.FacturesT.FindAsync(UserId);

                if (customer != null)
                {
                    string message = "Are you sure you want Delete your Account ?";
                    string title = "Delete";
                    var res = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var factures = await context.FacturesT
                                .Where(f => f.CustomerId == UserId)
                                .ToListAsync();

                            context.FacturesT.RemoveRange(factures);

                            context.CustomersT.Remove(customer);

                            await context.SaveChangesAsync();
                            
                            var loginWindow = new Login();
                            loginWindow.Show();
                            
                            foreach (Window window in Application.Current.Windows)
                            {
                                if (window is MainWindow)
                                {
                                    window.Close();
                                    break;
                                }
                            }
                        } catch (Exception ex)
                        {
                            MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
                        }

                    }
                }
                else
                {
                  MessageBox.Show("Client non trouvé.");
                }
            }
        }
    }
}