﻿// <auto-generated />
using LaBenVi.Products.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LaBenVi.Products.Migrations
{
    [DbContext(typeof(LaBenViDbContext))]
    [Migration("20240114155515_addProduct")]
    partial class addProduct
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LaBenVi.Products.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            CategoryName = "Women Fashion",
                            Description = "Beautiful golden ladies heels for grand occassions.",
                            ImageUrl = "https://www.brasslook.com/wp-content/uploads/2018/06/Gold-High-Heels-Shoes-Ideas-18.jpg",
                            Name = "High heels",
                            Price = 15.0
                        },
                        new
                        {
                            ProductId = 2,
                            CategoryName = "Cars",
                            Description = "The 2023 Bentley Mulliner Batur / Bacalar is a super-powered two-seater grand tourer that only Mulliner can create. The 2023 Bentley Mulliner batur is powered by a 6.0 liter, twin-turbocharged W12 engine that spins the rear wheels via an eight-speed automatic transmission.",
                            ImageUrl = "https://cdn.carbuzz.com/gallery-images/2023-bentley-batur-frontal-aspect-carbuzz-1030017.jpg",
                            Name = "2023 Bentley Continental GT Mulliner",
                            Price = 13.99
                        },
                        new
                        {
                            ProductId = 3,
                            CategoryName = "Women Fashion",
                            Description = "Classy designers white hand bag.",
                            ImageUrl = "https://ae01.alicdn.com/kf/HTB1nvnVnOCYBuNkHFCcq6AHtVXaV/white-handbag-2019-Elegant-Shoulder-Bag-Women-designer-handbags-high-quality-pu-leather-ladies-hand-bags.jpg",
                            Name = "Hand bag",
                            Price = 10.99
                        },
                        new
                        {
                            ProductId = 4,
                            CategoryName = "Men Fashion",
                            Description = "Designers suit for men.",
                            ImageUrl = "https://www.bing.com/images/search?view=detailV2&ccid=ITKK39of&id=4C3CA5085BD9EBDE7828194F715EA487657B0168&thid=OIP.ITKK39ofynUuKucW5DBIlgAAAA&mediaurl=https%3A%2F%2Fi0.wp.com%2Fwww.theunstitchd.com%2Fwp-content%2Fuploads%2F2016%2F05%2Fmen-black-suit.jpg%3Fw%3D1000&exph=713&expw=333&q=classy+male+suit&simid=607999857431368482&form=IRPRST&ck=61CABCB39CDED823D2BF81B9AE1F7829&selectedindex=0&itb=0&ajaxhist=0&ajaxserp=0&vt=0&sim=11&cdnurl=https%3A%2F%2Fth.bing.com%2Fth%2Fid%2FR.21328adfda1fca752e2ae716e4304896%3Frik%3DaAF7ZYekXnFPGQ%26pid%3DImgRaw%26r%3D0",
                            Name = "Pav Bhaji",
                            Price = 15.0
                        },
                        new
                        {
                            ProductId = 5,
                            CategoryName = "Dessert",
                            Description = "CHicken and vegie pizza just for your taste buds.",
                            ImageUrl = "https://picfiles.alphacoders.com/303/thumb-1920-303287.jpg",
                            Name = "Pizza",
                            Price = 15.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
