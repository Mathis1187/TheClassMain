using System.Windows.Data;

namespace TheClassMain.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Model;
    using TheClassMain.Query;
    using TheClassMain.Service;
    using TheClassMain.ViewModel;
    public class CategorieViewModel : ViewModelBase
    {
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        public Categories selectedCategorie;

        public string name;

        public bool isActive;

        public int userCategorieId;
        
        private string _categoriesFilter = string.Empty;
        private Visibility _btnVisibility = Visibility.Hidden;
        
        public ICollectionView CategoriesCollectionView { get; }
        public ObservableCollection<Categories> CategorieList => categoriesList;
        
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }
        public bool IsActive
        {
            get => isActive;
            set { isActive = value; OnPropertyChanged(); }
        }

        public Categories SelectedCategorie
        {
            get => selectedCategorie;
            set { selectedCategorie = value; OnPropertyChanged(); LoadSelectedCategorie(); }
        }

        public int UserCategorieId
        {
            get => userCategorieId;
            set { userCategorieId = value; OnPropertyChanged(); }
        }
        
        public Visibility BtnVisibility
        {
            get => _btnVisibility;
            set { _btnVisibility = value; OnPropertyChanged(); }
        }
        
        public string CategoriesFilter
        {
            get => _categoriesFilter;
            set
            {
                _categoriesFilter = value;
                OnPropertyChanged(nameof(CategoriesFilter));
                CategoriesCollectionView.Refresh();
            }
        }

        public CategorieViewModel()
        {
            CategoriesCollectionView = CollectionViewSource.GetDefaultView(categoriesList);
            CategoriesCollectionView.Filter = FilterCategories;
        }
        
        private bool FilterCategories(object obj)
        {
            if (obj is Categories category)
            {
                return (category.Name?.IndexOf(CategoriesFilter, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }
        
        public async Task AddCategorie()
        {
            if (!ValidateInputs()) return;

            try
            {
                using var context = new TableContext();

                int userCategorieCount = await context.CategoriesT
                    .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId)
                    .CountAsync();

                var newCategorie = new Categories
                {
                    Name = Name,
                    IsActive = IsActive,
                    UserCategorieId = userCategorieCount + 1,
                    CustomerId = Session.CurrentCustomer.CustomerId
                };

                context.CategoriesT.Add(newCategorie);
                await context.SaveChangesAsync();
                await LoadCategories();
                ClearInputs();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de l'ajout de la catégorie : {ex.Message}");
            }
        }
        public async Task UpdateCategorie()
        {
            if (SelectedCategorie == null || !ValidateInputs())
            {
                MessageBox.Show($"Please select a categorie first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                using var context = new TableContext();
                var categorieToUpdate = await context.CategoriesT.FindAsync(SelectedCategorie.CategorieId);
                if (categorieToUpdate != null)
                {
                    categorieToUpdate.Name = Name;
                    categorieToUpdate.IsActive = IsActive;
                    await context.SaveChangesAsync();
                    await LoadCategories();
                    ClearInputs();
                }
            }
        }
        public async Task DeleteCategorie()
        {
            if (SelectedCategorie == null)
            {
                MessageBox.Show($"Please select a categorie first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                string message = "Are you sure you want to delete this category?";
                string title = "Delete";
                using var context = new TableContext();
                var categorieToDelete = await context.CategoriesT.FindAsync(SelectedCategorie.CategorieId);
                if (categorieToDelete != null)
                {
                    var res = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        context.CategoriesT.Remove(categorieToDelete);
                        await context.SaveChangesAsync();
                        await LoadCategories();
                        ClearInputs();
                    }
                }
            }
        }
        public async Task LoadCategories()
        {
            try
            {
                using var context = new TableContext();
                var cats = await context.CategoriesT
                    .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId)
                    .ToListAsync();

                categoriesList.Clear();
                foreach (var cat in cats)
                    categoriesList.Add(cat);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors du chargement des catégories : {ex.Message}");
            }
        }

        public void LoadSelectedCategorie()
        {
            if (SelectedCategorie != null)
            {
                BtnVisibility = Visibility.Visible;
                Name = SelectedCategorie.Name;
                IsActive = SelectedCategorie.IsActive;
            }
        }
        
        public bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public void ClearInputs()
        {
            Name = string.Empty;
            IsActive = false;
            BtnVisibility = Visibility.Hidden;
        }


    }
}
