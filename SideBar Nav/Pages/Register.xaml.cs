using System.Windows;
using System.Windows.Input;
using TheClassMain.ViewModel;

namespace TheClassMain.Pages
{
    public partial class Register : Window
    {
        
        private RegisterViewModel viewModel => (RegisterViewModel)DataContext;
        public Register()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel();
        }

        private void Login_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}