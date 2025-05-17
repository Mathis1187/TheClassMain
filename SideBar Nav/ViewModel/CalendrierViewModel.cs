using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Service;
using TheClassMain.Query;

namespace TheClassMain.ViewModel
{
    public class CalendrierViewModel : ViewModelBase
    {
        private ObservableCollection<Factures> _facturesList = new ObservableCollection<Factures>();
        public ObservableCollection<Factures> FacturesList
        {
            get => _facturesList;
            set => SetProperty(ref _facturesList, value);
        }

        private ObservableCollection<Factures> _facturesDuJour = new ObservableCollection<Factures>();
        public ObservableCollection<Factures> FacturesDuJour
        {
            get => _facturesDuJour;
            set => SetProperty(ref _facturesDuJour, value);
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (SetProperty(ref _selectedDate, value))
                    FiltrerFacturesParDate();
            }
        }

        public CalendrierViewModel()
        {
            _ = ChargerFacturesAsync();
        }

        private async Task ChargerFacturesAsync()
        {
            using var context = new TableContext();
            var factures = await context.FacturesT
                .Where(f => f.CustomerId == Session.CurrentCustomer.CustomerId)
                .Include(f => f.Categorie)
                .ToListAsync();

            FacturesList = new ObservableCollection<Factures>(factures);
            FiltrerFacturesParDate();
        }

        private void FiltrerFacturesParDate()
        {
            if (SelectedDate is DateTime date)
            {
                var found = FacturesList
                    .Where(f => f.Date.Date == date.Date)
                    .ToList();

                FacturesDuJour = new ObservableCollection<Factures>(found);
            }
            else
            {
                FacturesDuJour.Clear();
            }
        }
    }
}
