using Budget_Man.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Budget_Man.Server
{
    public class ApplicationDbContext :  IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<FixedExpense> FixedExpense { get; set; }
        public DbSet<Expenses> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.Entity<Category>();
            modelBuilder.Entity<FixedExpense>();
            // modelBuilder.Entity<Category>().HasData(
            //  new Category { Id = 1, Name = "Food" },
            //  new Category { Id = 2, Name = "Fuel" },
            //  new Category { Id = 3, Name = "Entertainment" }
            // );

        //     modelBuilder.Entity<FixedExpense>().HasData(
        //     new FixedExpense { Id = 1, Name = "Phone", Amount = 10 },
        //     new FixedExpense { Id = 2, Name = "Gym", Amount = 20 }
        //    );
        }

    }
}