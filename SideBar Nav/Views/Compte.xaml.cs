using System.Windows;

namespace TheClassMain.Views
{
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Compte : Page
    {
        private readonly CompteViewModel viewModel;
        public Compte()
        {
            InitializeComponent();
            viewModel = new CompteViewModel();
            DataContext = viewModel;
        }

        private async void Delete_Account(object sender, System.Windows.RoutedEventArgs e)
        {
            await viewModel.DeleteCustomer();
        }        
        
        private async void ChangeEmail_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await viewModel.UpdateCustomerEmail();
        }        
        
        private async void ChangePasswd_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await viewModel.UpdateCustomerPwd();
        }        
        
        private async void ChangeUsername_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            await viewModel.UpdateCustomerUsername();
        }
    }
}