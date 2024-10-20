﻿// <auto-generated />
using System;
using Enigmatry.Entry.Blueprint.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enigmatry.Entry.Blueprint.Data.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Authorization.Permission", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Permission");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "UsersRead"
                        },
                        new
                        {
                            Id = 2,
                            Name = "UsersWrite"
                        },
                        new
                        {
                            Id = 10,
                            Name = "ProductsRead"
                        },
                        new
                        {
                            Id = 11,
                            Name = "ProductsWrite"
                        },
                        new
                        {
                            Id = 12,
                            Name = "ProductsDelete"
                        });
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Authorization.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0"),
                            Name = "SystemAdmin"
                        });
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<float?>("Discount")
                        .HasColumnType("real");

                    b.Property<DateOnly?>("ExpiresOn")
                        .HasColumnType("date");

                    b.Property<bool>("FreeShipping")
                        .HasColumnType("bit");

                    b.Property<bool>("HasDiscount")
                        .HasColumnType("bit");

                    b.Property<string>("InfoLink")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<Guid>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("UserStatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedById");

                    b.HasIndex("UserStatusId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc"),
                            CreatedById = new Guid("dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc"),
                            CreatedOn = new DateTimeOffset(new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            EmailAddress = "N/A",
                            FullName = "System User",
                            RoleId = new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0"),
                            UpdatedById = new Guid("dfb44aa8-bfc9-4d95-8f45-ed6da241dcfc"),
                            UpdatedOn = new DateTimeOffset(new DateTime(2024, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            UserStatusId = 1
                        });
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Users.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("UserStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Active Status Description",
                            Name = "Active"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Inactive Status Description",
                            Name = "Inactive"
                        });
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations.RolePermission", b =>
                {
                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PermissionId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermission");

                    b.HasData(
                        new
                        {
                            PermissionId = 1,
                            RoleId = new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0")
                        },
                        new
                        {
                            PermissionId = 2,
                            RoleId = new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0")
                        },
                        new
                        {
                            PermissionId = 10,
                            RoleId = new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0")
                        },
                        new
                        {
                            PermissionId = 11,
                            RoleId = new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0")
                        },
                        new
                        {
                            PermissionId = 12,
                            RoleId = new Guid("028e686d-51de-4dd9-91e9-dfb5ddde97d0")
                        });
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Products.Product", b =>
                {
                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Users.User", b =>
                {
                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Authorization.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Users.UserStatus", "UserStatus")
                        .WithMany()
                        .HasForeignKey("UserStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserStatus");
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Infrastructure.Data.Configurations.RolePermission", b =>
                {
                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Authorization.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enigmatry.Entry.Blueprint.Domain.Authorization.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Enigmatry.Entry.Blueprint.Domain.Authorization.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
