namespace TheClassMain.Query
{
    using Microsoft.EntityFrameworkCore;
    using TheClassMain.Model;

    public class TableContext : DbContext
    {
        public DbSet<Categories> CategoriesT { get; set; }

        public DbSet<Factures> FacturesT { get; set; }

        public DbSet<Customer> CustomersT { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDB)\MSSQLLocalDB;Database=TheClassMainDB;Trusted_Connection=True",
                options => options.EnableRetryOnFailure());
        }

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

        public void CreateDatabase()
        {
            this.Database.EnsureCreated();
        }
    }
}
