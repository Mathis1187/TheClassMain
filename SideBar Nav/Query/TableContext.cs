namespace TheClassMain.Query
{
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Composants;

    /// <summary>
    /// Defines the <see cref="TableContext" />
    /// </summary>
    public class TableContext : DbContext
    {
        /// <summary>
        /// Gets or sets the CategoriesT
        /// </summary>
        public DbSet<Categories> CategoriesT { get; set; }

        /// <summary>
        /// Gets or sets the FacturesT
        /// </summary>
        public DbSet<Factures> FacturesT { get; set; }

        /// <summary>
        /// The OnConfiguring
        /// </summary>
        /// <param name="optionsBuilder">The optionsBuilder<see cref="DbContextOptionsBuilder"/></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDB)\MSSQLLocalDB;Database=TheClassMainDB;Trusted_Connection=True",
                options => options.EnableRetryOnFailure());
        }

        /// <summary>
        /// The CreateDatabase
        /// </summary>
        public void CreateDatabase()
        {
            // Crée la base de données si elle n'existe pas
            this.Database.EnsureCreated();
        }
    }
}
