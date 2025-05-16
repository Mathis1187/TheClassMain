namespace TheClassMain.Pages
{
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Settings : Page
    {

        private readonly FactureViewModel viewModel = new();
        public Settings()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
