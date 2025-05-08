using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Query;
using TheClassMain.Service;

namespace TheClassMain.ViewModel
{
    public class FactureViewModel : ViewModelBase
    {
        private ObservableCollection<Factures> facturesList = new ObservableCollection<Factures>();
        private ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();
        private Factures selectedFacture;
        private Categories selectedCategorie;
        private string description;
        private string montant;
        private DateTime? date;
        private int userFactureId;
        private string _facturesFilter = string.Empty;
        private Visibility _btnVisibility = Visibility.Hidden;
        public ICollectionView FacturesCollectionView { get; }
        
        public string FacturesFilter
        {
            get => _facturesFilter;
            set
            {
                _facturesFilter = value;
                OnPropertyChanged(nameof(FacturesFilter));
                FacturesCollectionView.Refresh();
            }
        }

        public FactureViewModel()
        {
            FacturesCollectionView = CollectionViewSource.GetDefaultView(facturesList);
            FacturesCollectionView.Filter = FilterFactures;
        }

        private bool FilterFactures(object obj)
        {
            if (obj is Factures facture)
            {
                return (facture.Description?.IndexOf(FacturesFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                    || (facture.CategorieName?.IndexOf(FacturesFilter, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return false;
        }

        public ObservableCollection<Factures> FacturesList => facturesList;
        public ObservableCollection<Categories> CategoriesList => categoriesList;

        public Factures SelectedFacture
        {
            get => selectedFacture;
            set { selectedFacture = value; OnPropertyChanged(); LoadSelectedFacture(); }
        }

        public Categories SelectedCategorie
        {
            get => selectedCategorie;
            set { selectedCategorie = value; OnPropertyChanged();}
        }

        public Visibility BtnVisibility
        {
            get => _btnVisibility;
            set { _btnVisibility = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        public string Montant
        {
            get => montant;
            set { montant = value; OnPropertyChanged(); }
        }

        public DateTime? Date
        {
            get => date;
            set { date = value; OnPropertyChanged(); }
        }

        public int UserFactureId
        {
            get => userFactureId;
            set { userFactureId = value; OnPropertyChanged(); }
        }

        public async Task LoadFactures()
        {
            using var context = new TableContext();
            var factures = await context.FacturesT
                .Where(f => f.CustomerId == Session.CurrentCustomer.CustomerId)
                .ToListAsync();

            facturesList.Clear();
            foreach (var facture in factures)
                facturesList.Add(facture);
        }

        public async Task LoadCategories()
        {
            using var context = new TableContext();
            var cats = await context.CategoriesT
                .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId && c.IsActive)
                .ToListAsync();

            categoriesList.Clear();
            foreach (var cat in cats)
                categoriesList.Add(cat);
        }

        public async Task AddFacture()
        {
            if (!ValidateInputs(out decimal parsedMontant))
                return;

            using var context = new TableContext();
            var userFactureCount = await context.FacturesT
                .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId)
                .CountAsync();

            var newFacture = new Factures
            {
                Description = Description,
                Montant = parsedMontant,
                Date = Date.Value,
                CategorieName = SelectedCategorie.Name,
                UserFactureId = userFactureCount + 1,
                CategorieId = SelectedCategorie.CategorieId,
                CustomerId = Session.CurrentCustomer.CustomerId
            };

            context.FacturesT.Add(newFacture);
            await context.SaveChangesAsync();
            await LoadFactures();
            ClearInputs();
        }

        public async Task UpdateFacture()
        {
            if (SelectedFacture == null || !ValidateInputs(out decimal parsedMontant))
            {
                MessageBox.Show($"Please select a facture first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                using var context = new TableContext();
                var factureToUpdate = await context.FacturesT.FindAsync(SelectedFacture.FactureId);
                if (factureToUpdate != null)
                {
                    factureToUpdate.Description = Description;
                    factureToUpdate.Montant = parsedMontant;
                    factureToUpdate.Date = Date.Value;
                    factureToUpdate.CategorieId = SelectedCategorie.CategorieId;
                    factureToUpdate.CategorieName = SelectedCategorie.Name;
                    await context.SaveChangesAsync();
                    await LoadFactures();
                    ClearInputs();
                }
            }
        }

        public async Task DeleteFacture()
        {
            if (SelectedFacture == null)
            {
                MessageBox.Show($"Please select a facture first", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                string message = "Are you sure you want to delete this facture?";
                string title = "Delete";
                using var context = new TableContext();
                var factureToDelete = await context.FacturesT.FindAsync(SelectedFacture.FactureId);
                if (factureToDelete != null)
                {
                    var res = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        context.FacturesT.Remove(factureToDelete);
                        await context.SaveChangesAsync();
                        await LoadFactures();
                        ClearInputs();
                    }
                }
            }
        }

        public bool ValidateInputs(out decimal parsedMontant)
        {
            parsedMontant = 0;
            if (string.IsNullOrWhiteSpace(Description) ||
                string.IsNullOrWhiteSpace(Montant) ||
                !decimal.TryParse(Montant, out parsedMontant) ||
                Date == null || SelectedCategorie == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs correctement.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public void LoadSelectedFacture()
        {
            if (SelectedFacture != null)
            {
                BtnVisibility = Visibility.Visible;
                Description = SelectedFacture.Description;
                Montant = SelectedFacture.Montant.ToString("0.00");
                Date = SelectedFacture.Date;
                SelectedCategorie = CategoriesList.FirstOrDefault(c => c.CategorieId == SelectedFacture.CategorieId);
            }
        }

        public void ClearInputs()
        {
            Description = string.Empty;
            Montant = string.Empty;
            Date = null;
            SelectedCategorie = null;
            SelectedFacture = null;
            BtnVisibility = Visibility.Hidden;
        }
    }
}
