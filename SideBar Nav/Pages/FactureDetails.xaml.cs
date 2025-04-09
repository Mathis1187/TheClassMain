using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheClassMain.Composants;

namespace TheClassMain.Pages
{
    /// <summary>
    /// Logique d'interaction pour FactureDetails.xaml
    /// </summary>
    public partial class FactureDetails : Page
    {
        private Factures facture;
        public FactureDetails(Factures selectedFacture)
        {
            InitializeComponent();
            facture = selectedFacture;
            DataContext = facture;
        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
