namespace TheClassMain.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows;
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Model;
    using TheClassMain.Query;
    using TheClassMain.Service;
    public class FactureViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Factures> facturesList = new ObservableCollection<Factures>();

        public ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        public Factures selectedFacture;

        public Categories selectedCategorie;

        public string description;

        public string montant;

        public DateTime? date;

        public int userFactureId;
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
            set { selectedCategorie = value; OnPropertyChanged(); }
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
            using (var context = new TableContext())
            {
                var cats = await context.CategoriesT
                    .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId && c.IsActive)
                    .ToListAsync();

                categoriesList.Clear();
                foreach (var cat in cats)
                    categoriesList.Add(cat);
            }
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
                return;
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
        public async Task DeleteFacture()
        {
            if (SelectedFacture == null)
                return;
            using var context = new TableContext();
            var factureToDelete = await context.FacturesT.FindAsync(SelectedFacture.FactureId);
            if (factureToDelete != null)
            {
                context.FacturesT.Remove(factureToDelete);
                await context.SaveChangesAsync();
                await LoadFactures();
                ClearInputs();
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
