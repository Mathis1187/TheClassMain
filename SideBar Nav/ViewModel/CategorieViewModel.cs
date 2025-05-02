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

    /// <summary>
    /// Defines the <see cref="CategorieViewModel" />
    /// </summary>
    public class CategorieViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Defines the categoriesList
        /// </summary>
        public ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        /// <summary>
        /// Defines the selectedCategorie
        /// </summary>
        public Categories selectedCategorie;

        /// <summary>
        /// Defines the name
        /// </summary>
        public string name;

        /// <summary>
        /// Defines the isActive
        /// </summary>
        public bool isActive;

        /// <summary>
        /// Gets the CategorieList
        /// </summary>
        public ObservableCollection<Categories> CategorieList => categoriesList;

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsActive
        /// </summary>
        public bool IsActive
        {
            get => isActive;
            set { isActive = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the SelectedCategorie
        /// </summary>
        public Categories SelectedCategorie
        {
            get => selectedCategorie;
            set { selectedCategorie = value; OnPropertyChanged(); LoadSelectedCategorie(); }
        }

        /// <summary>
        /// Defines the PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The OnPropertyChanged
        /// </summary>
        /// <param name="name">The name<see cref="string"/></param>
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// The ValidateInputs
        /// </summary>
        /// <returns>The <see cref="bool"/></returns>
        public bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// The ClearInputs
        /// </summary>
        public void ClearInputs()
        {
            Name = string.Empty;
            IsActive = false;
        }

        /// <summary>
        /// The AddCategorie
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddCategorie()
        {
            if (!ValidateInputs()) return;

            var newCategorie = new Categories
            {
                Name = Name,
                IsActive = IsActive,
                CustomerId = Session.CurrentCustomer.CustomerId
            };

            try
            {
                using var context = new TableContext();
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

        /// <summary>
        /// The UpdateCategorie
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The DeleteCategorie
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The LoadCategories
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The LoadSelectedCategorie
        /// </summary>
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
