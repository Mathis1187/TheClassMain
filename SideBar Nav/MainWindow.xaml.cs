namespace TheClassMain
{
    using System.Windows;
    using System.Windows.Controls;
    using TheClassMain.Query;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            using (var context = new TableContext())
            {
                context.CreateDatabase();
            }
        }

        /// <summary>
        /// The sidebar_SelectionChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="SelectionChangedEventArgs"/></param>
        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = sidebar.SelectedItem as NavButton;

            navframe.Navigate(selected.Navlink);
        }
    }
}
