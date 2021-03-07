using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FullStack.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceHours = table.Column<double>(type: "float", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "DueDate", "IssueDate", "RefNumber" },
                values: new object[] { 1, new DateTime(2021, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV001" });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "DueDate", "IssueDate", "RefNumber" },
                values: new object[] { 5, new DateTime(2021, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "INV005" });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "InvoiceId", "ServiceDate", "ServiceDescription", "ServiceHours", "ServiceRate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "testing", 4.5, 123.45m },
                    { 2, 1, new DateTime(2021, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "optimization", 3.5, 199.99m },
                    { 3, 1, new DateTime(2021, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "development", 6.0, 320m },
                    { 4, 5, new DateTime(2021, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "optimization", 2.5, 199.99m },
                    { 5, 5, new DateTime(2021, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "development", 3.0, 320m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
