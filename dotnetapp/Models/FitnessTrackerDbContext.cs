using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
    public class FitnessTrackerDbContext : DbContext
    {
        public FitnessTrackerDbContext(DbContextOptions<FitnessTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<FitnessTracker> FitnessTrackers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FitnessTracker>(entity =>
    {
        entity.ToTable("FitnessTracker");

        entity.Property(e => e.Id)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.Workout_Date).HasColumnType("date");

        entity.Property(e => e.Steps).IsRequired();

        entity.Property(e => e.Distance_km).HasColumnType("decimal(10, 2)");

        entity.Property(e => e.CaloriesBurned).IsRequired();

        entity.Property(e => e.HeartRate).IsRequired();

        entity.Property(e => e.SleepDuration).HasColumnType("decimal(4, 2)");
    });
    }
    }
}
