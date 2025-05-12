using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Service;
using TheClassMain.Query;

namespace TheClassMain.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<Factures> facturesAujourdHui = new ObservableCollection<Factures>();
        private Factures selectedFacture;

        private int nombreFacturesAujourdhui;
        private decimal totalFactureAujourdhui;

        public ObservableCollection<Factures> FacturesAujourdhuiList => facturesAujourdHui;
        public ICollectionView FacturesCollectionView { get; }


        public int NombreFacturesAujourdhui
        {
            get => nombreFacturesAujourdhui;
            set { nombreFacturesAujourdhui = value; OnPropertyChanged(); }
        }

        public decimal TotalFactureAujourdhui
        {
            get => totalFactureAujourdhui;
            set { totalFactureAujourdhui = value; OnPropertyChanged(); }
        }

        public Factures SelectedFacture
        {
            get => selectedFacture;
            set { selectedFacture = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            FacturesCollectionView = CollectionViewSource.GetDefaultView(facturesAujourdHui);
            LoadFacturesAujourdhui();
        }


        public async void LoadFacturesAujourdhui()
        {
            using var context = new TableContext();
            var today = DateTime.Today;

            var factures = await context.FacturesT
                .Where(f => f.CustomerId == Session.CurrentCustomer.CustomerId &&
                            f.Date.Date == today)
                .ToListAsync();

            //factures.Sum(f => f.Montant);

            facturesAujourdHui.Clear();
            foreach (var f in factures)
                facturesAujourdHui.Add(f);


            NombreFacturesAujourdhui = factures.Count;
            TotalFactureAujourdhui = factures.Sum(f => f.Montant);

        }
    }
}
