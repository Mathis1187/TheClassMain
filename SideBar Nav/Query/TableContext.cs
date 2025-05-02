namespace TheClassMain.Query
{
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Model;

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
        /// Gets or sets the CustomersT
        /// </summary>
        public DbSet<Customer> CustomersT { get; set; }

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
        /// The OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="ModelBuilder"/></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factures>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Factures)
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Categories>()
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.Categories)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Factures>()
                .HasOne(f => f.Categorie)
                .WithMany(c => c.Factures)
                .HasForeignKey(f => f.CategorieId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        /// <summary>
        /// The CreateDatabase
        /// </summary>
        public void CreateDatabase()
        {
            this.Database.EnsureCreated();
        }
    }
}
