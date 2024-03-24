﻿// <auto-generated />
using System;
using ChocolateData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChocolateData.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChocolateDomain.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MainPhotoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("ChocolateDomain.Entities.PhotoEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Thumbnail")
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Photos", (string)null);
                });

            modelBuilder.Entity("ChocolateDomain.Entities.ProductEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Composition")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal?>("Height")
                        .HasPrecision(6, 2)
                        .HasColumnType("numeric(6,2)");

                    b.Property<Guid?>("MainPhotoId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasPrecision(8, 2)
                        .HasColumnType("numeric(8,2)");

                    b.Property<TimeSpan>("TimeToMake")
                        .HasColumnType("interval");

                    b.Property<int?>("Weight")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Width")
                        .HasPrecision(6, 2)
                        .HasColumnType("numeric(6,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MainPhotoId")
                        .IsUnique();

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("ChocolateDomain.Entities.PhotoEntity", b =>
                {
                    b.HasOne("ChocolateDomain.Entities.ProductEntity", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ChocolateDomain.Entities.ProductEntity", b =>
                {
                    b.HasOne("ChocolateDomain.Entities.CategoryEntity", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChocolateDomain.Entities.PhotoEntity", "MainPhoto")
                        .WithOne()
                        .HasForeignKey("ChocolateDomain.Entities.ProductEntity", "MainPhotoId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Category");

                    b.Navigation("MainPhoto");
                });

            modelBuilder.Entity("ChocolateDomain.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ChocolateDomain.Entities.ProductEntity", b =>
                {
                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
