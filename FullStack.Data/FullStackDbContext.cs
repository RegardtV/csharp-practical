using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using FullStack.Data.Entities;

namespace FullStack.Data
{
    public class FullStackDbContext: DbContext
    {
        //implement the DbContext
        public FullStackDbContext(DbContextOptions<FullStackDbContext> options)
            :base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seed the database with dummy data
            modelBuilder.Entity<Invoice>().HasData
            (
                new Invoice()
                {
                    Id = 1,
                    RefNumber = "INV001",
                    IssueDate = new DateTime(2021, 1, 26),
                    DueDate = new DateTime(2021, 2, 28),
                },
                new Invoice()
                {
                    Id = 5,
                    RefNumber = "INV005",
                    IssueDate = new DateTime(2021, 2, 23),
                    DueDate = new DateTime(2021, 3, 31),
                }
            );

            modelBuilder.Entity<InvoiceItem>().HasData(
               new InvoiceItem
               {
                   Id = 1,
                   ServiceDate = new DateTime(2021, 1, 10),
                   ServiceDescription = "testing",
                   ServiceRate = 123.45M,
                   ServiceHours = 4.5,
                   InvoiceId = 1
               },
               new InvoiceItem
               {
                   Id = 2,
                   ServiceDate = new DateTime(2021, 1, 12),
                   ServiceDescription = "optimization",
                   ServiceRate = 199.99M,
                   ServiceHours = 3.5,
                   InvoiceId = 1
               },
               new InvoiceItem
               {
                   Id = 3,
                   ServiceDate = new DateTime(2021, 1, 22),
                   ServiceDescription = "development",
                   ServiceRate = 320M,
                   ServiceHours = 6,
                   InvoiceId = 1
               },
               new InvoiceItem
               {
                   Id = 4,
                   ServiceDate = new DateTime(2021, 2, 11),
                   ServiceDescription = "optimization",
                   ServiceRate = 199.99M,
                   ServiceHours = 2.5,
                   InvoiceId = 5
               },
               new InvoiceItem
               {
                   Id = 5,
                   ServiceDate = new DateTime(2021, 2, 20),
                   ServiceDescription = "development",
                   ServiceRate = 320M,
                   ServiceHours = 3,
                   InvoiceId = 5
               }
               );

            base.OnModelCreating(modelBuilder);
        }
    }
}
