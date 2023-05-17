using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIdTicketAndAddQRCodeContentInCheckin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_Ticket_IdTicket",
                table: "Checkin");

            migrationBuilder.DropIndex(
                name: "IX_Checkin_IdTicket",
                table: "Checkin");

            migrationBuilder.DropColumn(
                name: "IdTicket",
                table: "Checkin");

            migrationBuilder.AddColumn<string>(
                name: "QRCodeContent",
                table: "Checkin",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QRCodeContent",
                table: "Checkin");

            migrationBuilder.AddColumn<Guid>(
                name: "IdTicket",
                table: "Checkin",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_IdTicket",
                table: "Checkin",
                column: "IdTicket");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_Ticket_IdTicket",
                table: "Checkin",
                column: "IdTicket",
                principalTable: "Ticket",
                principalColumn: "IdTicket",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
