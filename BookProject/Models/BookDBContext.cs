using System;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Models
{
   public class BookDBContext : DbContext
{
    public DbSet<Book> Books { get; set; }
}
}