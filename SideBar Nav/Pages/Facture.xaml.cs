using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TheClassMain.Composants;
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
        private void FacturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FacturesListBox.SelectedItem is Factures selectedFacture)
            {
                NavigationService.Navigate(new FactureDetails(selectedFacture));
            }
        }
    }
}