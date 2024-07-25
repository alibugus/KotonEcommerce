using EcommerceProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
		public DbSet<CategoryModel> Categories { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Seed data
			modelBuilder.Entity<CategoryModel>().HasData(
				new CategoryModel { Id = 1, Name = "Electronics" },
				new CategoryModel { Id = 2, Name = "Clothing" }
			);

			modelBuilder.Entity<ProductModel>().HasData(
				new ProductModel { Id = 1, Name = "Laptop", Price = 1000m, CategoryId = 1, ImageUrl = "laptop.jpg" },
				new ProductModel { Id = 2, Name = "T-Shirt", Price = 20m, CategoryId = 2, ImageUrl = "tshirt.jpg" }
			);

			// Specify the column type for the Price property
			modelBuilder.Entity<ProductModel>()
				.Property(p => p.Price)
				.HasColumnType("decimal(18,2)");
		}
	}
}
