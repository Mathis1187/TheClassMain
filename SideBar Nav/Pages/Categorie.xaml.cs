using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Composants;
using TheClassMain.Query;

namespace TheClassMain.Pages
{
    public partial class Categorie : Page
    {
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        public Categorie()
        {
            InitializeComponent();
            DataContext = this;
            
        }

        public ObservableCollection<Categories> CategorieList => categoriesList;

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategories();
        }

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
    }
}
