namespace TheClassMain.Pages
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Compte : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Compte"/> class.
        /// </summary>
        public Compte()
        {
            InitializeComponent();
        }

        private void Compte_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

    }
}
