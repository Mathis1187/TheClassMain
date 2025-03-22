using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheClassMain.Pages
{
    /// <summary>
    /// Logique d'interaction pour Facture.xaml
    /// </summary>
    public partial class Facture : Page
    {
        public Facture()
        {
            InitializeComponent();
        }

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ComboBox)?.SelectedItem;
            if (selectedItem != null)
            {
                MessageBox.Show($"Sélectionné : {selectedItem}");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
