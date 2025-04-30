namespace TheClassMain.Pages
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Composants;
    using TheClassMain.Query;

    /// <summary>
    /// Defines the <see cref="Facture" />
    /// </summary>
    public partial class Facture : Page
    {
        /// <summary>
        /// Defines the facturesList
        /// </summary>
        private ObservableCollection<Factures> facturesList = new ObservableCollection<Factures>();

        /// <summary>
        /// Defines the categoriesList
        /// </summary>
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        /// <summary>
        /// Defines the selectedFacture
        /// </summary>
        private Factures selectedFacture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Facture"/> class.
        /// </summary>
        public Facture()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Gets the FacturesList
        /// </summary>
        public ObservableCollection<Factures> FacturesList => facturesList;

        /// <summary>
        /// The Page_Loaded
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadFactures();
            await LoadCategories();
        }

        /// <summary>
        /// The LoadFactures
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The LoadCategories
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The Button_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(MontantTextBox.Text) ||
                !decimal.TryParse(MontantTextBox.Text, out decimal montant) ||
                DateFacturePicker.SelectedDate == null ||
                MyComboBox.SelectedItem is not Categories selectedCategorie)
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

        /// <summary>
        /// The FacturesListView_SelectionChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="SelectionChangedEventArgs"/></param>
        private void FacturesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFacture = FacturesListView.SelectedItem as Factures;

            if (selectedFacture != null)
            {
                DescriptionTextBox.Text = selectedFacture.Description;
                MontantTextBox.Text = selectedFacture.Montant.ToString("0.00");
                DateFacturePicker.SelectedDate = selectedFacture.Date;

                var categorie = categoriesList.FirstOrDefault(c => c.Name == selectedFacture.CategorieName);
                if (categorie != null)
                {
                    MyComboBox.SelectedItem = categorie;
                }
            }
        }

        /// <summary>
        /// The UpdateButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFacture == null)
            {
                MessageBox.Show("Veuillez d'abord sélectionner une facture.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ||
                string.IsNullOrWhiteSpace(MontantTextBox.Text) ||
                !decimal.TryParse(MontantTextBox.Text, out decimal montant) ||
                DateFacturePicker.SelectedDate == null ||
                MyComboBox.SelectedItem is not Categories selectedCategorie)
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new TableContext())
            {
                var factureToUpdate = await context.FacturesT.FindAsync(selectedFacture.Id);
                if (factureToUpdate != null)
                {
                    factureToUpdate.Description = DescriptionTextBox.Text;
                    factureToUpdate.Montant = montant;
                    factureToUpdate.Date = DateFacturePicker.SelectedDate.Value;
                    factureToUpdate.CategorieId = selectedCategorie.Id;
                    factureToUpdate.CategorieName = selectedCategorie.Name;

                    await context.SaveChangesAsync();
                }
            }

            MessageBox.Show("Facture mise à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            DescriptionTextBox.Clear();
            MontantTextBox.Clear();
            MyComboBox.SelectedIndex = -1;
            DateFacturePicker.SelectedDate = null;
            await LoadFactures();
        }

        /// <summary>
        /// The DeleteButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFacture == null)
            {
                MessageBox.Show("Veuillez d'abord sélectionner une facture à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette facture ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;

            using (var context = new TableContext())
            {
                var factureToDelete = await context.FacturesT.FindAsync(selectedFacture.Id);
                if (factureToDelete != null)
                {
                    context.FacturesT.Remove(factureToDelete);
                    await context.SaveChangesAsync();
                }
            }

            MessageBox.Show("Facture supprimée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            selectedFacture = null;
            DescriptionTextBox.Clear();
            MontantTextBox.Clear();
            MyComboBox.SelectedIndex = -1;
            DateFacturePicker.SelectedDate = null;

            await LoadFactures();
        }
    }
}
