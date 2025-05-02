namespace TheClassMain.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    /// <summary>
    /// Defines the <see cref="Categorie" />
    /// </summary>
    public partial class Categorie : Page
    {
        /// <summary>
        /// Gets the viewModel
        /// </summary>
        private CategorieViewModel viewModel => (CategorieViewModel)DataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Categorie"/> class.
        /// </summary>
        public Categorie()
        {
            InitializeComponent();
            DataContext = new CategorieViewModel();
            Loaded += Categorie_Loaded;
        }

        /// <summary>
        /// The Categorie_Loaded
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void Categorie_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadCategories();
        }

        /// <summary>
        /// The AddButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.AddCategorie();
        }

        /// <summary>
        /// The UpdateButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.UpdateCategorie();
        }

        /// <summary>
        /// The DeleteButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.DeleteCategorie();
        }
    }
}
