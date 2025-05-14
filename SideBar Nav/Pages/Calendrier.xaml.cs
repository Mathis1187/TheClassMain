using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Service;
using System.Windows.Data;
using TheClassMain.Query;

namespace TheClassMain.Pages
{
    public partial class Calendrier : Page
    {
        public ObservableCollection<Factures> FacturesList { get; set; } = new ObservableCollection<Factures>();
        public ObservableCollection<Factures> FacturesDuJour { get; set; } = new ObservableCollection<Factures>();

        public Calendrier()
        {
            InitializeComponent();
            DataContext = this;
            CalendrierCool26x.SelectedDatesChanged += DateChoisie;
            _ = ChargerFactures();
        }

        private async Task ChargerFactures()
        {
            using var context = new TableContext();
            var factures = await context.FacturesT
                .Where(f => f.CustomerId == Session.CurrentCustomer.CustomerId)
                .Include(f => f.Categorie)
                .ToListAsync();

            FacturesList.Clear();
            foreach (var f in factures)
                FacturesList.Add(f);
        }

        private void DateChoisie(object sender, SelectionChangedEventArgs e)
        {
            if (CalendrierCool26x.SelectedDate is DateTime date)
            {
                var facturesFound = FacturesList
                    .Where(f => f.Date.Date == date.Date)
                    .ToList();

                FacturesDuJour.Clear();
                foreach (var f in facturesFound)
                    FacturesDuJour.Add(f);
            }
        }
    }
}