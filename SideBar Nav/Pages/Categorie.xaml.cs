namespace TheClassMain.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using TheClassMain.ViewModel;
    public partial class Categorie : Page
    {
        private readonly CategorieViewModel viewModel;

        public Categorie()
        {
            InitializeComponent();
            viewModel = new CategorieViewModel();
            DataContext = viewModel;
            Loaded += Categorie_Loaded;
        }
        private async void Categorie_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadCategories();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.AddCategorie();
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.UpdateCategorie();
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.DeleteCategorie();
        }
        private void AnnulerButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearInputs();
        }
    }
}
