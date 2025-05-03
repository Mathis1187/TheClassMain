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
    public class CategorieViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        public Categories selectedCategorie;

        public string name;

        public bool isActive;

        public int userCategorieId;
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

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
            if (SelectedCategorie == null || !ValidateInputs()) return;

            try
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
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la mise à jour de la catégorie : {ex.Message}");
            }
        }
        public async Task DeleteCategorie()
        {
            if (SelectedCategorie == null) return;

            try
            {
                using var context = new TableContext();
                var categorieToDelete = await context.CategoriesT.FindAsync(SelectedCategorie.CategorieId);
                if (categorieToDelete != null)
                {
                    context.CategoriesT.Remove(categorieToDelete);
                    await context.SaveChangesAsync();
                    await LoadCategories();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erreur lors de la suppression de la catégorie : {ex.Message}");
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
                Name = SelectedCategorie.Name;
                IsActive = SelectedCategorie.IsActive;
            }
        }
    }
}
