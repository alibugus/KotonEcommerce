using EcommerceProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
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
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Category
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "Category1" },
                new CategoryModel { Id = 2, Name = "Category2" }
            );

            // Seed Brand
            modelBuilder.Entity<BrandModel>().HasData(
                new BrandModel { Id = 1, Name = "Brand1" },
                new BrandModel { Id = 2, Name = "Brand2" }
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
                }
            );
        }
    }
}