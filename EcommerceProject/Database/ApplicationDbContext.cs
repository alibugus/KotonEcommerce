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
        public DbSet<GuestCouponModel> GuestCoupons { get; set; }
        public DbSet<UserCouponModel> UserCoupons { get; set; }
        public DbSet<CouponModel> Coupons { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<GuestOrderModel> GuestOrders { get; set; }
        public DbSet<ModelInformationModel> ModelInformationModels { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
        public DbSet<GuestOrderDetailModel> GuestOrderDetails { get; set; }
        public DbSet<BrandModel> Brands { get; set; }
        public DbSet<ProductImageModel> ProductImages { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductSizeModel> ProductSizes { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Price Decimal Configuration
            modelBuilder.Entity<ProductModel>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Relationships
            modelBuilder.Entity<ProductModel>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSizeModel>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.ProductSizes)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Categories
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "T-Shirts" },
                new CategoryModel { Id = 2, Name = "Jeans" },
                new CategoryModel { Id = 3, Name = "Jackets" }


            );

            // Seed Brands
            modelBuilder.Entity<BrandModel>().HasData(
                new BrandModel { Id = 1, Name = "Brand1" },
                new BrandModel { Id = 2, Name = "Brand2" }
            );

            // Seed Coupons
            modelBuilder.Entity<CouponModel>().HasData(
                new CouponModel
                {
                    Id = 1,
                    Code = "WELCOME10",
                    DiscountAmount = 10m,
                    IsActive = true,
                    ExpiryDate = DateTime.Now.AddMonths(1)
                },
                new CouponModel
                {
                    Id = 2,
                    Code = "SUMMER20",
                    DiscountAmount = 20m,
                    IsActive = true,
                    ExpiryDate = DateTime.Now.AddMonths(2)
                },
                new CouponModel
                {
                    Id = 3,
                    Code = "FALL30",
                    DiscountAmount = 30m,
                    IsActive = true,
                    ExpiryDate = DateTime.Now.AddMonths(3)
                }
            );

            // Seed Model Information
            modelBuilder.Entity<ModelInformationModel>().HasData(
                new ModelInformationModel
                {
                    Id = 1,
                    Height = "180",
                    JeansSize = "32",
                    ShirtSize = "M",
                    ChestSize = "100",
                    HipSize = "95"
                },
                new ModelInformationModel
                {
                    Id = 2,
                    Height = "175",
                    JeansSize = "30",
                    ShirtSize = "S",
                    ChestSize = "95",
                    HipSize = "90"
                }
            );

            // Seed Products
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel
                {
                    Id = 1,
                    Name = "Casual T-Shirt",
                    Price = 19.99m,
                    Description = "A comfortable and stylish T-Shirt.",
                    StockQuantity = 50,
                    Size = "M,L,XL",
                    Color = "Red,Blue,Green",
                    CategoryId = 1,
                    BrandId = 1,
                    ImageUrl = "tshirt1.jpg",
                    ModelInformationId = 1,
                    Rating = 4,
                    ReviewCount = 10
                },
                new ProductModel
                {
                    Id = 8,
                    Name = "Sport T-Shirt",
                    Price = 19.99m,
                    Description = "A comfortable and stylish T-Shirt.",
                    StockQuantity = 50,
                    Size = "M,L,XL",
                    Color = "Red,Blue,Green",
                    CategoryId = 1,
                    BrandId = 2,
                    ImageUrl = "tshirt1.jpg",
                    ModelInformationId = 1,
                    Rating = 4,
                    ReviewCount = 10
                },
                new ProductModel
                {
                    Id = 2,
                    Name = "Classic Jeans",
                    Price = 49.99m,
                    Description = "Durable and stylish jeans for everyday wear.",
                    StockQuantity = 30,
                    Size = "30,32,34",
                    Color = "Blue,Black",
                    CategoryId = 2,
                    BrandId = 2,
                    ImageUrl = "jeans1.jpg",
                    ModelInformationId = 2,
                    Rating = 5,
                    ReviewCount = 20
                },
                 new ProductModel
                 {
                     Id = 3,
                     Name = "Casual T-Shirt",
                     Price = 29.99m,
                     Description = "A comfortable and stylish T-Shirt.",
                     StockQuantity = 50,
                     Size = "M,L,XL",
                     Color = "Red,Blue,Green",
                     CategoryId = 1,
                     BrandId = 1,
                     ImageUrl = "tshirt1.jpg",
                     ModelInformationId = 1,
                     Rating = 4,
                     ReviewCount = 10
                 },
                 new ProductModel
                 {
                     Id = 4,
                     Name = "Casual Jacket",
                     Price = 99.99m,
                     Description = "A comfortable and stylish Jacket.",
                     StockQuantity = 50,
                     Size = "M,L,XL",
                     Color = "Red,Blue,Green",
                     CategoryId = 3,
                     BrandId = 1,
                     ImageUrl = "tshirt1.jpg",
                     ModelInformationId = 1,
                     Rating = 4,
                     ReviewCount = 10
                 },
                 new ProductModel
                 {
                     Id = 5,
                     Name = "Sport Jacket",
                     Price = 29.99m,
                     Description = "A comfortable and stylish Jacket.",
                     StockQuantity = 50,
                     Size = "M,L,XL",
                     Color = "Red,Blue,Green",
                     CategoryId = 3,
                     BrandId = 1,
                     ImageUrl = "tshirt1.jpg",
                     ModelInformationId = 1,
                     Rating = 4,
                     ReviewCount = 10
                 },
                 new ProductModel
                 {
                     Id = 6,
                     Name = "Classic Jacket",
                     Price = 129.99m,
                     Description = "A comfortable and stylish Jacket.",
                     StockQuantity = 50,
                     Size = "M,L,XL",
                     Color = "Red,Blue,Green",
                     CategoryId = 3,
                     BrandId = 1,
                     ImageUrl = "tshirt1.jpg",
                     ModelInformationId = 1,
                     Rating = 4,
                     ReviewCount = 10
                 },
                 new ProductModel
                 {
                     Id = 7,
                     Name = "Kürk Ceket",
                     Price = 169.99m,
                     Description = "A comfortable and stylish Jacket.",
                     StockQuantity = 50,
                     Size = "M,L,XL",
                     Color = "Red,Blue,Green",
                     CategoryId = 3,
                     BrandId = 1,
                     ImageUrl = "tshirt1.jpg",
                     ModelInformationId = 1,
                     Rating = 4,
                     ReviewCount = 10
                 }
            );
            modelBuilder.Entity<ProductSizeModel>().HasData(
               new ProductSizeModel { Id = 10, ProductId = 1, Size = "M", StockQuantity = 20 },
               new ProductSizeModel { Id = 11, ProductId = 1, Size = "L", StockQuantity = 20 },
               new ProductSizeModel { Id = 12, ProductId = 1, Size = "XL", StockQuantity = 10 },
               new ProductSizeModel { Id = 13, ProductId = 2, Size = "30", StockQuantity = 10 },
               new ProductSizeModel { Id = 14, ProductId = 2, Size = "32", StockQuantity = 10 },
               new ProductSizeModel { Id = 15, ProductId = 2, Size = "34", StockQuantity = 10 },
               new ProductSizeModel { Id = 16, ProductId = 3, Size = "M", StockQuantity = 20 },
               new ProductSizeModel { Id = 17, ProductId = 3, Size = "L", StockQuantity = 20 },
               new ProductSizeModel { Id = 18, ProductId = 3, Size = "XL", StockQuantity = 10 }
           );

            // Seed Product Images
            modelBuilder.Entity<ProductImageModel>().HasData(
                new ProductImageModel
                {
                    Id = 1,
                    ProductId = 1,
                    ImageUrl = "img/shop-details/product-big.png"
                },
                new ProductImageModel
                {
                    Id = 2,
                    ProductId = 1,
                    ImageUrl = "img/shop-details/product-big-2.png"
                },
                new ProductImageModel
                {
                    Id = 3,
                    ProductId = 2,
                    ImageUrl = "img/shop-details/product-big-3.png"
                },
                new ProductImageModel
                {
                    Id = 4,
                    ProductId = 2,
                    ImageUrl = "img/shop-details/product-big-4.png"
                }
            );
        }

    }
}