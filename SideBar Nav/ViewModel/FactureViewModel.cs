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

    /// <summary>
    /// Defines the <see cref="FactureViewModel" />
    /// </summary>
    public class FactureViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Defines the facturesList
        /// </summary>
        public ObservableCollection<Factures> facturesList = new ObservableCollection<Factures>();

        /// <summary>
        /// Defines the categoriesList
        /// </summary>
        public ObservableCollection<Categories> categoriesList = new ObservableCollection<Categories>();

        /// <summary>
        /// Defines the selectedFacture
        /// </summary>
        public Factures selectedFacture;

        /// <summary>
        /// Defines the selectedCategorie
        /// </summary>
        public Categories selectedCategorie;

        /// <summary>
        /// Defines the description
        /// </summary>
        public string description;

        /// <summary>
        /// Defines the montant
        /// </summary>
        public string montant;

        /// <summary>
        /// Defines the date
        /// </summary>
        public DateTime? date;

        /// <summary>
        /// Gets the FacturesList
        /// </summary>
        public ObservableCollection<Factures> FacturesList => facturesList;

        /// <summary>
        /// Gets the CategoriesList
        /// </summary>
        public ObservableCollection<Categories> CategoriesList => categoriesList;

        /// <summary>
        /// Gets or sets the SelectedFacture
        /// </summary>
        public Factures SelectedFacture
        {
            get => selectedFacture;
            set { selectedFacture = value; OnPropertyChanged(); LoadSelectedFacture(); }
        }

        /// <summary>
        /// Gets or sets the SelectedCategorie
        /// </summary>
        public Categories SelectedCategorie
        {
            get => selectedCategorie;
            set { selectedCategorie = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description
        {
            get => description;
            set { description = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the Montant
        /// </summary>
        public string Montant
        {
            get => montant;
            set { montant = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the Date
        /// </summary>
        public DateTime? Date
        {
            get => date;
            set { date = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// The LoadFactures
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The LoadCategories
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task LoadCategories()
        {
            using (var context = new TableContext())
            {
                var cats = await context.CategoriesT
                    .Where(c => c.CustomerId == Session.CurrentCustomer.CustomerId)
                    .ToListAsync();

                categoriesList.Clear();
                foreach (var cat in cats)
                    categoriesList.Add(cat);
            }
        }

        /// <summary>
        /// The AddFacture
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddFacture()
        {
            if (!ValidateInputs(out decimal parsedMontant))
                return;

            var newFacture = new Factures
            {
                Description = Description,
                Montant = parsedMontant,
                Date = Date.Value,
                CategorieName = SelectedCategorie.Name,
                CategorieId = SelectedCategorie.CategorieId,
                CustomerId = Session.CurrentCustomer.CustomerId
            };

            using var context = new TableContext();
            context.FacturesT.Add(newFacture);
            await context.SaveChangesAsync();
            await LoadFactures();
            ClearInputs();
        }

        /// <summary>
        /// The UpdateFacture
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The DeleteFacture
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
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

        /// <summary>
        /// The ValidateInputs
        /// </summary>
        /// <param name="parsedMontant">The parsedMontant<see cref="decimal"/></param>
        /// <returns>The <see cref="bool"/></returns>
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

        /// <summary>
        /// The LoadSelectedFacture
        /// </summary>
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

        /// <summary>
        /// The ClearInputs
        /// </summary>
        public void ClearInputs()
        {
            Description = string.Empty;
            Montant = string.Empty;
            Date = null;
            SelectedCategorie = null;
            SelectedFacture = null;
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
    }
}
