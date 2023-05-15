using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndDeleteNumberSlotInEventLocationAndAddNumberSlotToPriceTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberSlot",
                table: "EventLocation");

            migrationBuilder.AddColumn<int>(
                name: "NumberSlot",
                table: "PriceTicket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "EventLocation",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberSlot",
                table: "PriceTicket");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "EventLocation");

            migrationBuilder.AddColumn<int>(
                name: "NumberSlot",
                table: "EventLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
