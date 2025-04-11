using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TheClassMain.Composants;

namespace TheClassMain.Pages
{
    public partial class Facture : Page
    {
        private ObservableCollection<Factures> facturesList = new ObservableCollection<Factures>();

        public Facture()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<Factures> FacturesList
        {
            get { return facturesList; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(MontantTextBox.Text) ||
                !decimal.TryParse(MontantTextBox.Text, out decimal montant) ||
                DateFacturePicker.SelectedDate == null ||
                MyComboBox.SelectedIndex <= 0)
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Factures newFacture = new Factures
            {
                Description = DescriptionTextBox.Text,
                Montant = montant,
                Categorie = (MyComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Date = DateFacturePicker.SelectedDate.Value
            };

            facturesList.Add(newFacture);

            DescriptionTextBox.Clear();
            MontantTextBox.Clear();
            MyComboBox.SelectedIndex = 0;
            DateFacturePicker.SelectedDate = null;
        }

        private void FacturesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FacturesListView.SelectedItem is Factures selectedFacture)
            {
                NavigationService.Navigate(new FactureDetails(selectedFacture));
            }
        }
    }
}
