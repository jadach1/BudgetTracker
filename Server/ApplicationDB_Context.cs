using Budget_Man.Models;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Server {
    public class ApplicationDbContext : DbContext{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        {
            
        }

        public DbSet<Category> Categories {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Category>().HasData(
            new Category {Id=1,Name="Food"},
            new Category {Id=2,Name="Fuel"},
            new Category {Id=3,Name="Entertainment"}
           );
        }

    }
}