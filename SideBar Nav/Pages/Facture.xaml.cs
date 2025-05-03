namespace TheClassMain.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Facture : Page
    {
        private FactureViewModel viewModel => (FactureViewModel)DataContext;

        public Facture()
        {
            InitializeComponent();
            DataContext = new FactureViewModel();
            Loaded += Facture_Loaded;
        }

        private async void Facture_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadCategories();
            await viewModel.LoadFactures();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.AddFacture();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.UpdateFacture();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.DeleteFacture();
        }
    }
}
