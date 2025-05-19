namespace TheClassMain.Pages
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using TheClassMain.Model;
    using TheClassMain.Query;
    using TheClassMain.Service;

    public partial class Historique : Page
    {
        public ObservableCollection<Factures> toutttlesfactures { get; set; } = new ObservableCollection<Factures>();
        public Historique()
        {
            InitializeComponent();
            DataContext = this;
            chargerlhistorique();
        }
        private async void chargerlhistorique()
        {
            using var context = new TableContext();

            var touttttt = await context.FacturesT
                .Include(i => i.Categorie)
                .ToListAsync();

            foreach (var i in touttttt)
            {
                if (i.CustomerId == Session.CurrentCustomer.CustomerId)
                {
                    toutttlesfactures.Add(i);
                }
            }
        }
    }
}
