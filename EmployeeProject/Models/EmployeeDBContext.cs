using System;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.Models
{
   public class EmployeeDBContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
}
}