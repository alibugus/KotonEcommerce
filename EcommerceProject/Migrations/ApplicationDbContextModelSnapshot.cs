﻿// <auto-generated />
using EcommerceProject.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerceProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EcommerceProject.Models.BrandModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Brand1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Brand2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Brand3"
                        });
                });

            modelBuilder.Entity("EcommerceProject.Models.CategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Category1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Category2"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Category3"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Category4"
                        });
                });

            modelBuilder.Entity("EcommerceProject.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            CategoryId = 1,
                            Color = "Red",
                            Description = "Description1",
                            ImageUrl = "image1.jpg",
                            Name = "Product1",
                            Price = 10.0m,
                            Size = "M",
                            StockQuantity = 100
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 2,
                            CategoryId = 2,
                            Color = "Blue",
                            Description = "Description2",
                            ImageUrl = "image2.jpg",
                            Name = "Product2",
                            Price = 20.0m,
                            Size = "L",
                            StockQuantity = 200
                        },
                        new
                        {
                            Id = 3,
                            BrandId = 2,
                            CategoryId = 1,
                            Color = "Green",
                            Description = "Description3",
                            ImageUrl = "image3.jpg",
                            Name = "Product3",
                            Price = 15.0m,
                            Size = "S",
                            StockQuantity = 150
                        },
                        new
                        {
                            Id = 4,
                            BrandId = 1,
                            CategoryId = 2,
                            Color = "Yellow",
                            Description = "Description4",
                            ImageUrl = "image4.jpg",
                            Name = "Product4",
                            Price = 25.0m,
                            Size = "XL",
                            StockQuantity = 250
                        },
                        new
                        {
                            Id = 5,
                            BrandId = 1,
                            CategoryId = 3,
                            Color = "Black",
                            Description = "Description5",
                            ImageUrl = "image5.jpg",
                            Name = "Product5",
                            Price = 30.0m,
                            Size = "M",
                            StockQuantity = 300
                        },
                        new
                        {
                            Id = 6,
                            BrandId = 3,
                            CategoryId = 4,
                            Color = "White",
                            Description = "Description6",
                            ImageUrl = "image6.jpg",
                            Name = "Product6",
                            Price = 35.0m,
                            Size = "L",
                            StockQuantity = 350
                        });
                });

            modelBuilder.Entity("EcommerceProject.Models.ProductModel", b =>
                {
                    b.HasOne("EcommerceProject.Models.BrandModel", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceProject.Models.CategoryModel", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EcommerceProject.Models.BrandModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EcommerceProject.Models.CategoryModel", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
