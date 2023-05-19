using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerVerify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ManagerVerify",
                columns: table => new
                {
                    IdVerifyCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerVerify", x => x.IdVerifyCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManagerVerify");
        }
    }
}
