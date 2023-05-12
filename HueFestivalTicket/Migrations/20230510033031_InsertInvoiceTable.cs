using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    /// <inheritdoc />
    public partial class InsertInvoiceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Customer_IdCustomer",
                table: "Ticket");

            migrationBuilder.RenameColumn(
                name: "IdCustomer",
                table: "Ticket",
                newName: "IdInvoice");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_IdCustomer",
                table: "Ticket",
                newName: "IX_Ticket_IdInvoice");

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    IdInvoice = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCustomer = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.IdInvoice);
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_IdCustomer",
                        column: x => x.IdCustomer,
                        principalTable: "Customer",
                        principalColumn: "IdCustomer",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_IdCustomer",
                table: "Invoice",
                column: "IdCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Invoice_IdInvoice",
                table: "Ticket",
                column: "IdInvoice",
                principalTable: "Invoice",
                principalColumn: "IdInvoice",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Invoice_IdInvoice",
                table: "Ticket");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.RenameColumn(
                name: "IdInvoice",
                table: "Ticket",
                newName: "IdCustomer");

            migrationBuilder.RenameIndex(
                name: "IX_Ticket_IdInvoice",
                table: "Ticket",
                newName: "IX_Ticket_IdCustomer");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Customer_IdCustomer",
                table: "Ticket",
                column: "IdCustomer",
                principalTable: "Customer",
                principalColumn: "IdCustomer",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
