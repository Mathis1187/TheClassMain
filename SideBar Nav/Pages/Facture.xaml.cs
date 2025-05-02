namespace TheClassMain.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    /// <summary>
    /// Defines the <see cref="Facture" />
    /// </summary>
    public partial class Facture : Page
    {
        /// <summary>
        /// Gets the viewModel
        /// </summary>
        private FactureViewModel viewModel => (FactureViewModel)DataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Facture"/> class.
        /// </summary>
        public Facture()
        {
            InitializeComponent();
            DataContext = new FactureViewModel();
            Loaded += Facture_Loaded;
        }

        /// <summary>
        /// The Facture_Loaded
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void Facture_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadCategories();
            await viewModel.LoadFactures();
        }

        /// <summary>
        /// The AddButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.AddFacture();
        }

        /// <summary>
        /// The UpdateButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.UpdateFacture();
        }

        /// <summary>
        /// The DeleteButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            await viewModel.DeleteFacture();
        }
    }
}
