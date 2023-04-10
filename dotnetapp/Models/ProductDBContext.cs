using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using dotnetapp.Models;

public partial class ProductDBContext : DbContext
{
    public ProductDBContext(DbContextOptions options):base(options)
    {

    }
    public DbSet<Product> Products {get; set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasColumnType("decimal(18,2)");
}

}