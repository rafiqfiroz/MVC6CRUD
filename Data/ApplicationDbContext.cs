using Microsoft.EntityFrameworkCore;
using MVC6CRUD.Models;

namespace MVC6CRUD.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
                
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
     .Property(e => e.CreateDate)
     .HasColumnType("datetime2");

            modelBuilder.Entity<Product>()
                .Property(e => e.UpdatedDate)
                .HasColumnType("datetime2");

        }
    }
}
