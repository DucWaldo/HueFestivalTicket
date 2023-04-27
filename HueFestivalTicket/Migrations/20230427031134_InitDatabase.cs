using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    IdAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TimeJoined = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.IdAccount);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    IdCustomer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCard = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.IdCustomer);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    IdEvent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusTicket = table.Column<bool>(type: "bit", nullable: false),
                    TypeEvent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.IdEvent);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Support",
                columns: table => new
                {
                    IdSuport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Support", x => x.IdSuport);
                });

            migrationBuilder.CreateTable(
                name: "TypeLocation",
                columns: table => new
                {
                    IdTypeLocation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeLocation", x => x.IdTypeLocation);
                });

            migrationBuilder.CreateTable(
                name: "TypeTicket",
                columns: table => new
                {
                    IdTypeTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTicket", x => x.IdTypeTicket);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    IdNews = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAccount = table.Column<int>(type: "int", nullable: false),
                    AccountIdAccount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.IdNews);
                    table.ForeignKey(
                        name: "FK_News_Account_AccountIdAccount",
                        column: x => x.AccountIdAccount,
                        principalTable: "Account",
                        principalColumn: "IdAccount");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdAccount = table.Column<int>(type: "int", nullable: false),
                    AccountIdAccount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_User_Account_AccountIdAccount",
                        column: x => x.AccountIdAccount,
                        principalTable: "Account",
                        principalColumn: "IdAccount");
                });

            migrationBuilder.CreateTable(
                name: "ImageEvent",
                columns: table => new
                {
                    IdImageEvent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEvent = table.Column<int>(type: "int", nullable: false),
                    EventIdEvent = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageEvent", x => x.IdImageEvent);
                    table.ForeignKey(
                        name: "FK_ImageEvent_Event_EventIdEvent",
                        column: x => x.EventIdEvent,
                        principalTable: "Event",
                        principalColumn: "IdEvent");
                });

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    IdAccountRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAccount = table.Column<int>(type: "int", nullable: false),
                    AccountIdAccount = table.Column<int>(type: "int", nullable: true),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    RoleIdRole = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => x.IdAccountRole);
                    table.ForeignKey(
                        name: "FK_AccountRole_Account_AccountIdAccount",
                        column: x => x.AccountIdAccount,
                        principalTable: "Account",
                        principalColumn: "IdAccount");
                    table.ForeignKey(
                        name: "FK_AccountRole_Role_RoleIdRole",
                        column: x => x.RoleIdRole,
                        principalTable: "Role",
                        principalColumn: "IdRole");
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    IdLocation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTypeLocation = table.Column<int>(type: "int", nullable: false),
                    TypeLocationIdTypeLocation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.IdLocation);
                    table.ForeignKey(
                        name: "FK_Location_TypeLocation_TypeLocationIdTypeLocation",
                        column: x => x.TypeLocationIdTypeLocation,
                        principalTable: "TypeLocation",
                        principalColumn: "IdTypeLocation");
                });

            migrationBuilder.CreateTable(
                name: "EventLocation",
                columns: table => new
                {
                    IdEventLocation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberSlot = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdEvent = table.Column<int>(type: "int", nullable: false),
                    EventIdEvent = table.Column<int>(type: "int", nullable: true),
                    IdLocation = table.Column<int>(type: "int", nullable: false),
                    LocationIdLocation = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocation", x => x.IdEventLocation);
                    table.ForeignKey(
                        name: "FK_EventLocation_Event_EventIdEvent",
                        column: x => x.EventIdEvent,
                        principalTable: "Event",
                        principalColumn: "IdEvent");
                    table.ForeignKey(
                        name: "FK_EventLocation_Location_LocationIdLocation",
                        column: x => x.LocationIdLocation,
                        principalTable: "Location",
                        principalColumn: "IdLocation");
                });

            migrationBuilder.CreateTable(
                name: "PriceTicket",
                columns: table => new
                {
                    IdPriceTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdEventLocation = table.Column<int>(type: "int", nullable: false),
                    EventLocationIdEventLocation = table.Column<int>(type: "int", nullable: true),
                    IdTypeTicket = table.Column<int>(type: "int", nullable: false),
                    TypeTicketIdTypeTicket = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTicket", x => x.IdPriceTicket);
                    table.ForeignKey(
                        name: "FK_PriceTicket_EventLocation_EventLocationIdEventLocation",
                        column: x => x.EventLocationIdEventLocation,
                        principalTable: "EventLocation",
                        principalColumn: "IdEventLocation");
                    table.ForeignKey(
                        name: "FK_PriceTicket_TypeTicket_TypeTicketIdTypeTicket",
                        column: x => x.TypeTicketIdTypeTicket,
                        principalTable: "TypeTicket",
                        principalColumn: "IdTypeTicket");
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    IdTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdEventLocation = table.Column<int>(type: "int", nullable: false),
                    EventLocationIdEventLocation = table.Column<int>(type: "int", nullable: true),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    CustomerIdCustomer = table.Column<int>(type: "int", nullable: true),
                    IdTypeTicket = table.Column<int>(type: "int", nullable: false),
                    TypeTicketIdTypeTicket = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.IdTicket);
                    table.ForeignKey(
                        name: "FK_Ticket_Customer_CustomerIdCustomer",
                        column: x => x.CustomerIdCustomer,
                        principalTable: "Customer",
                        principalColumn: "IdCustomer");
                    table.ForeignKey(
                        name: "FK_Ticket_EventLocation_EventLocationIdEventLocation",
                        column: x => x.EventLocationIdEventLocation,
                        principalTable: "EventLocation",
                        principalColumn: "IdEventLocation");
                    table.ForeignKey(
                        name: "FK_Ticket_TypeTicket_TypeTicketIdTypeTicket",
                        column: x => x.TypeTicketIdTypeTicket,
                        principalTable: "TypeTicket",
                        principalColumn: "IdTypeTicket");
                });

            migrationBuilder.CreateTable(
                name: "Checkin",
                columns: table => new
                {
                    IdCheckin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeCheckin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    IdAccount = table.Column<int>(type: "int", nullable: false),
                    AccountIdAccount = table.Column<int>(type: "int", nullable: true),
                    IdTicket = table.Column<int>(type: "int", nullable: false),
                    TicketIdTicket = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkin", x => x.IdCheckin);
                    table.ForeignKey(
                        name: "FK_Checkin_Account_AccountIdAccount",
                        column: x => x.AccountIdAccount,
                        principalTable: "Account",
                        principalColumn: "IdAccount");
                    table.ForeignKey(
                        name: "FK_Checkin_Ticket_TicketIdTicket",
                        column: x => x.TicketIdTicket,
                        principalTable: "Ticket",
                        principalColumn: "IdTicket");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_AccountIdAccount",
                table: "AccountRole",
                column: "AccountIdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RoleIdRole",
                table: "AccountRole",
                column: "RoleIdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_AccountIdAccount",
                table: "Checkin",
                column: "AccountIdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_TicketIdTicket",
                table: "Checkin",
                column: "TicketIdTicket");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_EventIdEvent",
                table: "EventLocation",
                column: "EventIdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_LocationIdLocation",
                table: "EventLocation",
                column: "LocationIdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_ImageEvent_EventIdEvent",
                table: "ImageEvent",
                column: "EventIdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_Location_TypeLocationIdTypeLocation",
                table: "Location",
                column: "TypeLocationIdTypeLocation");

            migrationBuilder.CreateIndex(
                name: "IX_News_AccountIdAccount",
                table: "News",
                column: "AccountIdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTicket_EventLocationIdEventLocation",
                table: "PriceTicket",
                column: "EventLocationIdEventLocation");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTicket_TypeTicketIdTypeTicket",
                table: "PriceTicket",
                column: "TypeTicketIdTypeTicket");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CustomerIdCustomer",
                table: "Ticket",
                column: "CustomerIdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_EventLocationIdEventLocation",
                table: "Ticket",
                column: "EventLocationIdEventLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TypeTicketIdTypeTicket",
                table: "Ticket",
                column: "TypeTicketIdTypeTicket");

            migrationBuilder.CreateIndex(
                name: "IX_User_AccountIdAccount",
                table: "User",
                column: "AccountIdAccount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.DropTable(
                name: "Checkin");

            migrationBuilder.DropTable(
                name: "ImageEvent");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "PriceTicket");

            migrationBuilder.DropTable(
                name: "Support");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "EventLocation");

            migrationBuilder.DropTable(
                name: "TypeTicket");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "TypeLocation");
        }
    }
}
