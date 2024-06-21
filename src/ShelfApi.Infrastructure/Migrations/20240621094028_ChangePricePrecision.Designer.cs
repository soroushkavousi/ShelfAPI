﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShelfApi.Infrastructure.Data.ShelfApiDb;

#nullable disable

namespace ShelfApi.Infrastructure.Migrations
{
    [DbContext(typeof(ShelfApiDbContext))]
    [Migration("20240621094028_ChangePricePrecision")]
    partial class ChangePricePrecision
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:CollationDefinition:case_insensitive", "und-u-ks-level2,und-u-ks-level2,icu,False")
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<ulong>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(100);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnOrder(102)
                        .UseCollation("case_insensitive");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnOrder(103)
                        .UseCollation("case_insensitive");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<short>("RoleId")
                        .HasColumnType("smallint")
                        .HasColumnOrder(101);

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<ulong>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnOrder(100);

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnOrder(102)
                        .UseCollation("case_insensitive");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnOrder(103)
                        .UseCollation("case_insensitive");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(101);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<ulong>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnOrder(100)
                        .UseCollation("case_insensitive");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnOrder(101)
                        .UseCollation("case_insensitive");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnOrder(102)
                        .UseCollation("case_insensitive");

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(1);

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<ulong>", b =>
                {
                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(100);

                    b.Property<short>("RoleId")
                        .HasColumnType("smallint")
                        .HasColumnOrder(101);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<ulong>", b =>
                {
                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(100);

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnOrder(101)
                        .UseCollation("case_insensitive");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnOrder(102)
                        .UseCollation("case_insensitive");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnOrder(103)
                        .UseCollation("case_insensitive");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("ShelfApi.Domain.BaseDataAggregate.ProjectSetting", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("smallint")
                        .HasColumnOrder(101);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<string>("Data")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("{}")
                        .HasColumnOrder(102)
                        .UseCollation("case_insensitive");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.HasKey("Id");

                    b.ToTable("ProjectSettings");
                });

            modelBuilder.Entity("ShelfApi.Domain.OrderAggregate.Order", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<decimal>("ListPrice")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnOrder(102);

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<decimal>("NetPrice")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnOrder(104);

                    b.Property<byte>("State")
                        .HasColumnType("smallint")
                        .HasColumnOrder(101);

                    b.Property<decimal>("TaxPrice")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnOrder(103);

                    b.Property<decimal>("UserId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ShelfApi.Domain.OrderAggregate.OrderLine", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<decimal>("OrderId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(100);

                    b.Property<decimal>("ProductId")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(101);

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnOrder(102);

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnOrder(103);

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("ShelfApi.Domain.ProductAggregate.Product", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(1);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnOrder(100)
                        .UseCollation("case_insensitive");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(12,2)")
                        .HasColumnOrder(101);

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnOrder(102);

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShelfApi.Domain.UserAggregate.Role", b =>
                {
                    b.Property<short>("Id")
                        .HasColumnType("smallint")
                        .HasColumnOrder(1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnOrder(105)
                        .UseCollation("case_insensitive");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(103)
                        .UseCollation("case_insensitive");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(104)
                        .UseCollation("case_insensitive");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("ShelfApi.Domain.UserAggregate.User", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnOrder(1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnOrder(117);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnOrder(111)
                        .UseCollation("case_insensitive");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1000)
                        .HasDefaultValueSql("now() at time zone 'utc'");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(106)
                        .UseCollation("case_insensitive");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnOrder(108);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnOrder(100);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnOrder(116);

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(115);

                    b.Property<DateTime?>("ModifiedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1001);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(107)
                        .UseCollation("case_insensitive");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(105)
                        .UseCollation("case_insensitive");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnOrder(109)
                        .UseCollation("case_insensitive");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnOrder(112)
                        .UseCollation("case_insensitive");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnOrder(113);

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnOrder(110)
                        .UseCollation("case_insensitive");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnOrder(114);

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnOrder(104)
                        .UseCollation("case_insensitive");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<ulong>", b =>
                {
                    b.HasOne("ShelfApi.Domain.UserAggregate.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<ulong>", b =>
                {
                    b.HasOne("ShelfApi.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<ulong>", b =>
                {
                    b.HasOne("ShelfApi.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<ulong>", b =>
                {
                    b.HasOne("ShelfApi.Domain.UserAggregate.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShelfApi.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<ulong>", b =>
                {
                    b.HasOne("ShelfApi.Domain.UserAggregate.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShelfApi.Domain.OrderAggregate.Order", b =>
                {
                    b.HasOne("ShelfApi.Domain.UserAggregate.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShelfApi.Domain.OrderAggregate.OrderLine", b =>
                {
                    b.HasOne("ShelfApi.Domain.OrderAggregate.Order", null)
                        .WithMany("Lines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShelfApi.Domain.ProductAggregate.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShelfApi.Domain.OrderAggregate.Order", b =>
                {
                    b.Navigation("Lines");
                });
#pragma warning restore 612, 618
        }
    }
}
