using Xunit;
using Microsoft.EntityFrameworkCore;
using TheClassMain.Model;
using TheClassMain.Query;
using System.Linq;

namespace TheClassMain.Tests
{
    public class CategorieTests
    {
        // Test : insertion d'une catégorie dans la DB en mémoire
        [Fact]
        public void CanInsertCategorieIntoInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("CategorieInsertTestDB")
                .Options;

            var categorie = new Categories
            {
                Name = "Transport"
            };

            using (var context = new TableContext(options))
            {
                context.CategoriesT.Add(categorie);
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var result = context.CategoriesT.FirstOrDefault(c => c.Name == "Transport");
                Assert.NotNull(result);
                Assert.Equal("Transport", result.Name);
            }
        }

        // Test logique : une catégorie sans nom ne devrait pas être considérée comme valide
        [Fact]
        public void Categorie_IsValid_ShouldReturnFalse_WhenNameIsEmpty()
        {
            var categorie = new Categories
            {
                Name = ""
            };

            bool isValid = !string.IsNullOrWhiteSpace(categorie.Name);

            Assert.False(isValid);
        }

        // Test : modification d’une catégorie existante
        [Fact]
        public void UpdateCategorie_ShouldModifyName()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("CategorieUpdateTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.CategoriesT.Add(new Categories { Name = "AncienNom" });
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var cat = context.CategoriesT.First();
                cat.Name = "NouveauNom";
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var updated = context.CategoriesT.First();
                Assert.Equal("NouveauNom", updated.Name);
            }
        }

        // Test : récupération de toutes les catégories
        [Fact]
        public void CanRetrieveAllCategories()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("CategorieListTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.CategoriesT.AddRange(
                    new Categories { Name = "Alimentation" },
                    new Categories { Name = "Santé" },
                    new Categories { Name = "Éducation" }
                );
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var list = context.CategoriesT.ToList();
                Assert.Equal(3, list.Count);
                Assert.Contains(list, c => c.Name == "Santé");
            }
        }
    }
}
