namespace TheClassMain.Views
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

        private void NavigateToCategorie_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Categorie());
        }

        private void NavigateToFacture_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Facture());
        }
    }
}
