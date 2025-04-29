using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Composants;
using TheClassMain.Query;

namespace TheClassMain.Pages
{
    public partial class Facture : Page
    {
        private ObservableCollection<Factures> facturesList = new ObservableCollection<Factures>();
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();
        public Facture()
        {
            InitializeComponent();
            DataContext = this;
        }
        public ObservableCollection<Factures> FacturesList => facturesList;
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadFactures();
            await LoadCategories();
        }
        private async Task LoadFactures()
        {
            using (var context = new TableContext())
            {
                var factures = await context.FacturesT.ToListAsync();
                facturesList.Clear();
                foreach (var facture in factures)
                {
                    facturesList.Add(facture);
                }
            }
        }
        private async Task LoadCategories()
        {
            using (var context = new TableContext())
            {
                var categories = await context.CategoriesT.Where(c => c.IsActive).ToListAsync();
                categoriesList.Clear();
                foreach (var categorie in categories)
                {
                    categoriesList.Add(categorie);
                }
                MyComboBox.ItemsSource = categoriesList;
                MyComboBox.DisplayMemberPath = "Name";
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(MontantTextBox.Text) ||
                !decimal.TryParse(MontantTextBox.Text, out decimal montant) ||
                DateFacturePicker.SelectedDate == null ||
                MyComboBox.SelectedItem is not Categories selectedCategorie ||
                string.IsNullOrWhiteSpace(selectedCategorie.Name))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Factures newFacture = new Factures
            {
                Name = "Facture " + DateTime.Now.ToString("yyyyMMddHHmmss"),
                Description = DescriptionTextBox.Text,
                Montant = montant,
                Date = DateFacturePicker.SelectedDate.Value,
                CategorieName = selectedCategorie.Name,
                CategorieId = selectedCategorie.Id
            };
            using (var context = new TableContext())
            {
                context.FacturesT.Add(newFacture);
                await context.SaveChangesAsync();
            }
            DescriptionTextBox.Clear();
            MontantTextBox.Clear();
            MyComboBox.SelectedIndex = -1;
            DateFacturePicker.SelectedDate = null;
            await LoadFactures();
        }
    }
}