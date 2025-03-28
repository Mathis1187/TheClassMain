using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
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
using TheClassMain.Composants;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheClassMain.Pages
{
    public partial class Facture : Page
    {

        private ObservableCollection<Factures> facturesList = new ObservableCollection<Factures>();

        public Facture()
        {
            InitializeComponent();
            FacturesListBox.ItemsSource = facturesList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(MontantTextBox.Text) ||
                !int.TryParse(MontantTextBox.Text, out int montant) ||
                DateFacturePicker.SelectedDate == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Factures newFacture = new Factures
            {
                Description = DescriptionTextBox.Text,
                Montant = montant,
                Date = DateFacturePicker.SelectedDate.Value

            };
            facturesList.Add(newFacture);
        }

        private void MyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ComboBox)?.SelectedItem;
            if (selectedItem != null)
            {
                MessageBox.Show($"Sélectionné : {selectedItem}");
            }
        }
    }
}
