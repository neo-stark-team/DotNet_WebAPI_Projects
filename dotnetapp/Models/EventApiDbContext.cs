using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class EventApiDbContext : DbContext
    {
        public EventApiDbContext(DbContextOptions<EventApiDbContext> options) : base(options)
        {
        }

        public DbSet<EventApi> EventApis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventApi>().ToTable("EventApi");
            modelBuilder.Entity<EventApi>().HasKey(t => t.Id);
            modelBuilder.Entity<EventApi>().Property(t => t.Start_Date).HasColumnType("datetime2");
            modelBuilder.Entity<EventApi>().Property(t => t.End_Date).HasColumnType("datetime2");
        }
    }
}
