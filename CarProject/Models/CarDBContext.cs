using System;
using Microsoft.EntityFrameworkCore;

namespace CarProject.Models
{
   public class CarDBContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
}
}