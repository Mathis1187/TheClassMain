using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TheClassMain.Service;

namespace TheClassMain.ViewModel
{
    class CompteViewModel : ViewModelBase
    {
        private string userUsername;


        public string UserUsername
        {
            get => userUsername;
            set { userUsername = value; OnPropertyChanged(); }
        }

        public CompteViewModel()
        {
            LoadUserInfo();
        }

        public void LoadUserInfo()
        {
            var user = Service.Session.CurrentCustomer;

            UserUsername = user.UserName;

        }
    }
}
