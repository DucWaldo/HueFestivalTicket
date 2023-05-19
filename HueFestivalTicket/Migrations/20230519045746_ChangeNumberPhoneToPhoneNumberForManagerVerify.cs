using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNumberPhoneToPhoneNumberForManagerVerify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberPhone",
                table: "ManagerVerify",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "ManagerVerify",
                newName: "NumberPhone");
        }
    }
}
