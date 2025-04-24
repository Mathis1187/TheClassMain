using Microsoft.EntityFrameworkCore;
using TheClassMain.Composants;

namespace TheClassMain.Query
{
    public class TableContext : DbContext
    {
        public DbSet<Categories> CategoriesT { get; set; }
        public DbSet<Factures> FacturesT { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDB)\MSSQLLocalDB;Database=TheClassMainDB;Trusted_Connection=True",
                options => options.EnableRetryOnFailure());
        }

        public void CreateDatabase()
        {
            // Crée la base de données si elle n'existe pas
            this.Database.EnsureCreated();
        }
    }
}
