using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductAPI.Model.Context
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(){}
        public MySqlContext(DbContextOptions<MySqlContext> options) 
        : base(options) {}

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 2,
                Name = "Name",
                Price = 29.90m,
                CategoryName = "Name",
                Description = "Description",
                ImageUrl = "teste"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Name",
                Price = 29.90m,
                CategoryName = "Name",
                Description = "Description",
                ImageUrl = "teste"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Name",
                Price = 29.90m,
                CategoryName = "Name",
                Description = "Description",
                ImageUrl = "teste"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Name",
                Price = 29.90m,
                CategoryName = "Name",
                Description = "Description",
                ImageUrl = "teste"
            });
        }
    }
}
