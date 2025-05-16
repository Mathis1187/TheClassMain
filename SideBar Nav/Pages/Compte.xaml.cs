namespace TheClassMain.Pages
{
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Compte : Page
    {
        private readonly CompteViewModel viewModel;
        public Compte()
        {
            InitializeComponent();
            DataContext = new CompteViewModel();
        }

        private async void Delete_Account(object sender, System.Windows.RoutedEventArgs e)
        {
            await viewModel.DeleteCustomer();
        }
    }
}