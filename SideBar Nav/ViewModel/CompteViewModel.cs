using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheClassMain.Pages;
using TheClassMain.Query;
using TheClassMain.Service;

namespace TheClassMain.ViewModel
{
    class CompteViewModel : ViewModelBase
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
            DeleteCustomer();

        }

        public void LoadUserInfo()
        {
            var user = Service.Session.CurrentCustomer;

            UserUsername = user.UserName;
            UserEmail = user.Email;
            UserPassword = user.Pwd;
            UserId = user.CustomerId;
        }



        public async Task DeleteCustomer()
        {
            using (var context = new TableContext())
            {
                var customer = await context.CustomersT.FindAsync(UserId);

                if (customer != null)
                {
                    MessageBox.Show("Client trouvé.");
                }
                else
                {
                    MessageBox.Show("Client non trouvé.");
                }
            }
        }



    }
}
