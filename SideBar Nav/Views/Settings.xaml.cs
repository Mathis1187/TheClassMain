namespace TheClassMain.Views
{
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Settings : Page
    {

        private readonly SettingsViewModel viewModel = new();
        public Settings()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
