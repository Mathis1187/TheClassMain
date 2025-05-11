namespace TheClassMain.Pages
{
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }
    }
}
