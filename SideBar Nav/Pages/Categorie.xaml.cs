using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TheClassMain.Composants;

namespace TheClassMain.Pages
{
    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    /// 
    public partial class Categorie : Page
    {
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();
        public Categorie()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ObservableCollection<Categories> CategorieList
        {
            get { return categoriesList; }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Categories newCategorie = new Categories
            {
                Name = NameTextBox.Text,
                Favorit = (bool)FavoritCheckBox.IsChecked
            };

            categoriesList.Add(newCategorie);

            NameTextBox.Clear();
        }

    }
}

