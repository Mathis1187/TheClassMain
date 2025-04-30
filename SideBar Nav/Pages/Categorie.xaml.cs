namespace TheClassMain.Pages
{
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Composants;
    using TheClassMain.Query;

    /// <summary>
    /// Defines the <see cref="Categorie" />
    /// </summary>
    public partial class Categorie : Page
    {
        /// <summary>
        /// Defines the categoriesList
        /// </summary>
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        /// <summary>
        /// Defines the selectedCategorie
        /// </summary>
        private Categories selectedCategorie;

        /// <summary>
        /// Initializes a new instance of the <see cref="Categorie"/> class.
        /// </summary>
        public Categorie()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Gets the CategorieList
        /// </summary>
        public ObservableCollection<Categories> CategorieList => categoriesList;

        /// <summary>
        /// The Page_Loaded
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategories();
        }

        /// <summary>
        /// The Button_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Categories newCategorie = new Categories
            {
                Name = NameTextBox.Text,
                IsActive = (bool)ActiveCheckBox.IsChecked
            };

            using (var context = new TableContext())
            {
                context.CategoriesT.Add(newCategorie);
                await context.SaveChangesAsync();
            }

            NameTextBox.Clear();
            ActiveCheckBox.IsChecked = false;
            await LoadCategories();
        }

        /// <summary>
        /// The LoadCategories
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        private async Task LoadCategories()
        {
            using (var context = new TableContext())
            {
                var cats = await context.CategoriesT.ToListAsync();
                categoriesList.Clear();
                foreach (var cat in cats)
                    categoriesList.Add(cat);
            }
        }

        /// <summary>
        /// The CategoriesListView_SelectionChanged
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="SelectionChangedEventArgs"/></param>
        private void CategoriesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCategorie = CategoriesListView.SelectedItem as Categories;

            if (selectedCategorie != null)
            {
                NameTextBox.Text = selectedCategorie.Name;
                ActiveCheckBox.IsChecked = selectedCategorie.IsActive;
            }
        }

        /// <summary>
        /// The UpdateButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCategorie == null)
            {
                MessageBox.Show("Veuillez d'abord sélectionner une catégorie.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || ActiveCheckBox.IsChecked == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new TableContext())
            {
                var categorieToUpdate = await context.CategoriesT.FindAsync(selectedCategorie.Id);
                if (categorieToUpdate != null)
                {
                    categorieToUpdate.Name = NameTextBox.Text;
                    categorieToUpdate.IsActive = (bool)ActiveCheckBox.IsChecked;

                    await context.SaveChangesAsync();
                }
            }

            MessageBox.Show("Catégorie mise à jour avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            NameTextBox.Clear();
            ActiveCheckBox.IsChecked = false;
            await LoadCategories();
        }

        /// <summary>
        /// The DeleteButton_Click
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/></param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/></param>
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCategorie == null)
            {
                MessageBox.Show("Veuillez d'abord sélectionner une catégorie à supprimer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette catégorie ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
                return;

            using (var context = new TableContext())
            {
                var categorieToDelete = await context.CategoriesT.FindAsync(selectedCategorie.Id);
                if (categorieToDelete != null)
                {
                    context.CategoriesT.Remove(categorieToDelete);
                    await context.SaveChangesAsync();
                }
            }

            MessageBox.Show("Catégorie supprimée avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
            selectedCategorie = null;
            NameTextBox.Clear();
            ActiveCheckBox.IsChecked = false;
            await LoadCategories();
        }
    }
}
