using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
            modelBuilder.Entity<Transaction>().HasKey(t => t.Id);
            modelBuilder.Entity<Transaction>().Property(t => t.TransactionDate).HasColumnType("datetime2");
            // modelBuilder.Entity<Transaction>().Property(t => t.TransactionTime).HasColumnType("time(0)");
            modelBuilder.Entity<Transaction>().Property(t => t.TotalPrice).HasColumnType("decimal(18,2)");
        }
    }
}
