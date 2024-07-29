using EcommerceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductModel>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade); // Eğer silme davranışı belirlemek istiyorsanız

            // Seed Category
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "Category1" },
                new CategoryModel { Id = 2, Name = "Category2" },
                new CategoryModel { Id = 3, Name = "Category3" },
                new CategoryModel { Id = 4, Name = "Category4" }
            );

            // Seed Brand
            modelBuilder.Entity<BrandModel>().HasData(
                new BrandModel { Id = 1, Name = "Brand1" },
                new BrandModel { Id = 2, Name = "Brand2" },
                new BrandModel { Id = 3, Name = "Brand3" }
            );

            // Seed Product
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel
                {
                    Id = 1,
                    Name = "Product1",
                    Price = 10.0m,
                    Description = "Description1",
                    StockQuantity = 100,
                    Size = "M",
                    Color = "Red",
                    CategoryId = 1,
                    BrandId = 1,
                    ImageUrl = "image1.jpg"
                },
                new ProductModel
                {
                    Id = 2,
                    Name = "Product2",
                    Price = 20.0m,
                    Description = "Description2",
                    StockQuantity = 200,
                    Size = "L",
                    Color = "Blue",
                    CategoryId = 2,
                    BrandId = 2,
                    ImageUrl = "image2.jpg"
                },
                new ProductModel
                {
                    Id = 3,
                    Name = "Product3",
                    Price = 15.0m,
                    Description = "Description3",
                    StockQuantity = 150,
                    Size = "S",
                    Color = "Green",
                    CategoryId = 1,
                    BrandId = 2,
                    ImageUrl = "image3.jpg"
                },
                new ProductModel
                {
                    Id = 4,
                    Name = "Product4",
                    Price = 25.0m,
                    Description = "Description4",
                    StockQuantity = 250,
                    Size = "XL",
                    Color = "Yellow",
                    CategoryId = 2,
                    BrandId = 1,
                    ImageUrl = "image4.jpg"
                },
                new ProductModel
                {
                    Id = 5,
                    Name = "Product5",
                    Price = 30.0m,
                    Description = "Description5",
                    StockQuantity = 300,
                    Size = "M",
                    Color = "Black",
                    CategoryId = 3,
                    BrandId = 1,
                    ImageUrl = "image5.jpg"
                },
                new ProductModel
                {
                    Id = 6,
                    Name = "Product6",
                    Price = 35.0m,
                    Description = "Description6",
                    StockQuantity = 350,
                    Size = "L",
                    Color = "White",
                    CategoryId = 4,
                    BrandId = 3,
                    ImageUrl = "image6.jpg"
                }
            );
        }
    }
}
