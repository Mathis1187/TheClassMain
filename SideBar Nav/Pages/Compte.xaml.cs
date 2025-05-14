namespace TheClassMain.Pages
{
    using System.Windows.Controls;
    using TheClassMain.ViewModel;

    public partial class Compte : Page
    {
        //private readonly CompteViewModel viewModel; 
        public Compte()
        {
            InitializeComponent();
            DataContext = new CompteViewModel();
        }
    }
}
