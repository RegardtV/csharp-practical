﻿// <auto-generated />
using System;
using FullStack.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FullStack.Data.Migrations
{
    [DbContext(typeof(FullStackDbContext))]
    [Migration("20210305063144_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FullStack.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FullStack.Data.Entities.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Invoices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreateDate = new DateTime(2021, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RefNumber = "INV001",
                            Total = 3175.49m
                        },
                        new
                        {
                            Id = 5,
                            CreateDate = new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DueDate = new DateTime(2021, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            RefNumber = "INV005",
                            Total = 1459.975m
                        });
                });

            modelBuilder.Entity("FullStack.Data.Entities.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<decimal>("ServiceCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("ServiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ServiceHours")
                        .HasColumnType("float");

                    b.Property<decimal>("ServiceRate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("InvoiceItems");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InvoiceId = 1,
                            ServiceCost = 555.525m,
                            ServiceDate = new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "testing",
                            ServiceHours = 4.5,
                            ServiceRate = 123.45m
                        },
                        new
                        {
                            Id = 2,
                            InvoiceId = 1,
                            ServiceCost = 699.9825m,
                            ServiceDate = new DateTime(2021, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "optimization",
                            ServiceHours = 3.5,
                            ServiceRate = 199.99m
                        },
                        new
                        {
                            Id = 3,
                            InvoiceId = 1,
                            ServiceCost = 1920m,
                            ServiceDate = new DateTime(2021, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "development",
                            ServiceHours = 6.0,
                            ServiceRate = 320m
                        },
                        new
                        {
                            Id = 4,
                            InvoiceId = 5,
                            ServiceCost = 499.9875m,
                            ServiceDate = new DateTime(2021, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "optimization",
                            ServiceHours = 2.5,
                            ServiceRate = 199.99m
                        },
                        new
                        {
                            Id = 5,
                            InvoiceId = 5,
                            ServiceCost = 960m,
                            ServiceDate = new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ServiceDescription = "development",
                            ServiceHours = 3.0,
                            ServiceRate = 320m
                        });
                });

            modelBuilder.Entity("FullStack.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FullStack.Data.Entities.InvoiceItem", b =>
                {
                    b.HasOne("FullStack.Data.Entities.Invoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");
                });

            modelBuilder.Entity("FullStack.Data.Entities.Invoice", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}