using Xunit;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Query;
using System;
using System.Linq;

namespace TheClassMain.Tests
{
    public class FactureTests
    {
        // Test : insertion d'une facture dans la DB en mémoire
        [Fact]
        public void CanInsertFactureIntoInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("InsertFactureTestDB")
                .Options;

            var facture = new Factures
            {
                Description = "Test facture",
                Montant = 49.99m,
                Date = DateTime.Today,
                CategorieId = 1,
                CategorieName = "Électricité",
                CustomerId = 1,
                UserFactureId = 1
            };

            using (var context = new TableContext(options))
            {
                context.FacturesT.Add(facture);
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var result = context.FacturesT.FirstOrDefault(f => f.Description == "Test facture");
                Assert.NotNull(result);
                Assert.Equal("Électricité", result.CategorieName);
            }
        }

        // Test : mise à jour d'une facture
        [Fact]
        public void UpdateFacture_ShouldModifyExistingFacture()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("UpdateFactureTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.FacturesT.Add(new Factures
                {
                    Description = "Initiale",
                    Montant = 10,
                    Date = DateTime.Today,
                    CategorieId = 1,
                    CategorieName = "Autre",
                    CustomerId = 1,
                    UserFactureId = 1
                });
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var facture = context.FacturesT.First();
                facture.Description = "Modifiée";
                facture.Montant = 99.99m;
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var facture = context.FacturesT.First();
                Assert.Equal("Modifiée", facture.Description);
                Assert.Equal(99.99m, facture.Montant);
            }
        }

        // Test : récupération de plusieurs factures d’un client
        [Fact]
        public void CanRetrieveAllFacturesForGivenCustomer()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("CustomerFacturesTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.FacturesT.AddRange(
                    new Factures { Description = "F1", Montant = 10, Date = DateTime.Today, CategorieId = 1, CategorieName = "X", CustomerId = 5, UserFactureId = 1 },
                    new Factures { Description = "F2", Montant = 20, Date = DateTime.Today, CategorieId = 2, CategorieName = "Y", CustomerId = 5, UserFactureId = 2 },
                    new Factures { Description = "Autre", Montant = 30, Date = DateTime.Today, CategorieId = 3, CategorieName = "Z", CustomerId = 9, UserFactureId = 3 }
                );
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var facturesClient5 = context.FacturesT.Where(f => f.CustomerId == 5).ToList();
                Assert.Equal(2, facturesClient5.Count);
                Assert.All(facturesClient5, f => Assert.Equal(5, f.CustomerId));
            }
        }

        // Test logique : validation des champs d’une facture (version invalide)
        [Fact]
        public void Facture_IsValid_ShouldReturnFalse_WhenFieldsAreMissing()
        {
            var facture = new Factures
            {
                Description = "",
                Montant = 0,
                CategorieName = null,
                CategorieId = 0
            };

            bool isValid =
                !string.IsNullOrWhiteSpace(facture.Description) &&
                facture.Montant > 0 &&
                !string.IsNullOrWhiteSpace(facture.CategorieName) &&
                facture.CategorieId > 0;

            Assert.False(isValid);
        }

        // Test logique : validation des champs d’une facture (version valide)
        [Fact]
        public void Facture_IsValid_ShouldReturnTrue_WhenAllFieldsAreValid()
        {
            var facture = new Factures
            {
                Description = "Internet",
                Montant = 75.5m,
                Date = DateTime.Today,
                CategorieId = 2,
                CategorieName = "Services",
                CustomerId = 1,
                UserFactureId = 1
            };

            bool isValid =
                !string.IsNullOrWhiteSpace(facture.Description) &&
                facture.Montant > 0 &&
                !string.IsNullOrWhiteSpace(facture.CategorieName) &&
                facture.CategorieId > 0;

            Assert.True(isValid);
        }
    }
}
