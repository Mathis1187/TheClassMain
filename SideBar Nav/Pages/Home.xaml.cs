namespace TheClassMain.Pages
{
    using System;
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Home : Page
    {

        public Home()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }

        private void NavigateToAccount_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Compte()); // Remplace par le nom exact de ta page
        }        
        
        private void NavigateToFacture_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Facture()); // Remplace par le nom exact de ta page
        }
    }
}
