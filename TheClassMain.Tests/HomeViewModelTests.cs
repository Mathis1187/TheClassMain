using Xunit;
using System;
using TheClassMain.Model;
using TheClassMain.ViewModel;
using System.Linq;

namespace TheClassMain.Tests
{
    public class HomeViewModelTests
    {
        // Test : les propriétés numériques doivent être initialisées à 0
        [Fact]
        public void HomeViewModel_InitialValues_ShouldBeZero()
        {
            var vm = new HomeViewModel(chargerFactures: false);

            Assert.Equal(0, vm.NombreFacturesAujourdhui);
            Assert.Equal(0, vm.NombreTotalFactures);
            Assert.Equal(0, vm.TotalFactureAujourdhui);
            Assert.Equal(0, vm.TotalFactures);
        }

        // Test : ajouter une facture met à jour la collection des factures du jour
        [Fact]
        public void AddingFacture_ShouldAppearInFacturesAujourdhuiList()
        {
            var vm = new HomeViewModel(chargerFactures: false);

            var facture = new Factures
            {
                Description = "Test",
                Montant = 50,
                Date = DateTime.Today
            };

            vm.FacturesAujourdhuiList.Add(facture);

            Assert.Contains(facture, vm.FacturesAujourdhuiList);
        }

        // Test : sélection d'une facture doit mettre à jour SelectedFacture
        [Fact]
        public void SelectedFacture_ShouldBeSetCorrectly()
        {
            var vm = new HomeViewModel(chargerFactures: false);

            var facture = new Factures
            {
                Description = "Test",
                Montant = 100
            };

            vm.SelectedFacture = facture;

            Assert.Equal(facture, vm.SelectedFacture);
        }

        // Test : le total des montants est correct après ajout manuel de factures dans la liste
        [Fact]
        public void TotalMontantManuel_ShouldBeCorrect_WhenAddingFacturesToList()
        {
            var vm = new HomeViewModel(chargerFactures: false);

            vm.FacturesAujourdhuiList.Add(new Factures { Montant = 20 });
            vm.FacturesAujourdhuiList.Add(new Factures { Montant = 30 });

            decimal totalManuel = vm.FacturesAujourdhuiList.Sum(f => f.Montant);

            Assert.Equal(50, totalManuel);
        }
    }
}
