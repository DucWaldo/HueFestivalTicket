using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFestivalTicket.Migrations
{
    /// <inheritdoc />
    public partial class DeleteAccountRoleAndFixSame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_Account_AccountIdAccount",
                table: "Checkin");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_Ticket_TicketIdTicket",
                table: "Checkin");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLocation_Event_EventIdEvent",
                table: "EventLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLocation_Location_LocationIdLocation",
                table: "EventLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageEvent_Event_EventIdEvent",
                table: "ImageEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_TypeLocation_TypeLocationIdTypeLocation",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Account_AccountIdAccount",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceTicket_EventLocation_EventLocationIdEventLocation",
                table: "PriceTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceTicket_TypeTicket_TypeTicketIdTypeTicket",
                table: "PriceTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Customer_CustomerIdCustomer",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_EventLocation_EventLocationIdEventLocation",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TypeTicket_TypeTicketIdTypeTicket",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Account_AccountIdAccount",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AccountIdAccount",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_CustomerIdCustomer",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_EventLocationIdEventLocation",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_TypeTicketIdTypeTicket",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_PriceTicket_EventLocationIdEventLocation",
                table: "PriceTicket");

            migrationBuilder.DropIndex(
                name: "IX_PriceTicket_TypeTicketIdTypeTicket",
                table: "PriceTicket");

            migrationBuilder.DropIndex(
                name: "IX_News_AccountIdAccount",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Location_TypeLocationIdTypeLocation",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_ImageEvent_EventIdEvent",
                table: "ImageEvent");

            migrationBuilder.DropIndex(
                name: "IX_EventLocation_EventIdEvent",
                table: "EventLocation");

            migrationBuilder.DropIndex(
                name: "IX_EventLocation_LocationIdLocation",
                table: "EventLocation");

            migrationBuilder.DropIndex(
                name: "IX_Checkin_AccountIdAccount",
                table: "Checkin");

            migrationBuilder.DropIndex(
                name: "IX_Checkin_TicketIdTicket",
                table: "Checkin");

            migrationBuilder.DropColumn(
                name: "AccountIdAccount",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CustomerIdCustomer",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EventLocationIdEventLocation",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "TypeTicketIdTypeTicket",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "EventLocationIdEventLocation",
                table: "PriceTicket");

            migrationBuilder.DropColumn(
                name: "TypeTicketIdTypeTicket",
                table: "PriceTicket");

            migrationBuilder.DropColumn(
                name: "AccountIdAccount",
                table: "News");

            migrationBuilder.DropColumn(
                name: "TypeLocationIdTypeLocation",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "EventIdEvent",
                table: "ImageEvent");

            migrationBuilder.DropColumn(
                name: "EventIdEvent",
                table: "EventLocation");

            migrationBuilder.DropColumn(
                name: "LocationIdLocation",
                table: "EventLocation");

            migrationBuilder.DropColumn(
                name: "AccountIdAccount",
                table: "Checkin");

            migrationBuilder.DropColumn(
                name: "TicketIdTicket",
                table: "Checkin");

            migrationBuilder.CreateIndex(
                name: "IX_User_IdAccount",
                table: "User",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_IdCustomer",
                table: "Ticket",
                column: "IdCustomer");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_IdEventLocation",
                table: "Ticket",
                column: "IdEventLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_IdTypeTicket",
                table: "Ticket",
                column: "IdTypeTicket");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTicket_IdEventLocation",
                table: "PriceTicket",
                column: "IdEventLocation");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTicket_IdTypeTicket",
                table: "PriceTicket",
                column: "IdTypeTicket");

            migrationBuilder.CreateIndex(
                name: "IX_News_IdAccount",
                table: "News",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Location_IdTypeLocation",
                table: "Location",
                column: "IdTypeLocation");

            migrationBuilder.CreateIndex(
                name: "IX_ImageEvent_IdEvent",
                table: "ImageEvent",
                column: "IdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_IdEvent",
                table: "EventLocation",
                column: "IdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_IdLocation",
                table: "EventLocation",
                column: "IdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_IdAccount",
                table: "Checkin",
                column: "IdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_IdTicket",
                table: "Checkin",
                column: "IdTicket");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_Account_IdAccount",
                table: "Checkin",
                column: "IdAccount",
                principalTable: "Account",
                principalColumn: "IdAccount",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_Ticket_IdTicket",
                table: "Checkin",
                column: "IdTicket",
                principalTable: "Ticket",
                principalColumn: "IdTicket",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocation_Event_IdEvent",
                table: "EventLocation",
                column: "IdEvent",
                principalTable: "Event",
                principalColumn: "IdEvent",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocation_Location_IdLocation",
                table: "EventLocation",
                column: "IdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageEvent_Event_IdEvent",
                table: "ImageEvent",
                column: "IdEvent",
                principalTable: "Event",
                principalColumn: "IdEvent",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_TypeLocation_IdTypeLocation",
                table: "Location",
                column: "IdTypeLocation",
                principalTable: "TypeLocation",
                principalColumn: "IdTypeLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Account_IdAccount",
                table: "News",
                column: "IdAccount",
                principalTable: "Account",
                principalColumn: "IdAccount",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceTicket_EventLocation_IdEventLocation",
                table: "PriceTicket",
                column: "IdEventLocation",
                principalTable: "EventLocation",
                principalColumn: "IdEventLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceTicket_TypeTicket_IdTypeTicket",
                table: "PriceTicket",
                column: "IdTypeTicket",
                principalTable: "TypeTicket",
                principalColumn: "IdTypeTicket",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Customer_IdCustomer",
                table: "Ticket",
                column: "IdCustomer",
                principalTable: "Customer",
                principalColumn: "IdCustomer",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_EventLocation_IdEventLocation",
                table: "Ticket",
                column: "IdEventLocation",
                principalTable: "EventLocation",
                principalColumn: "IdEventLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TypeTicket_IdTypeTicket",
                table: "Ticket",
                column: "IdTypeTicket",
                principalTable: "TypeTicket",
                principalColumn: "IdTypeTicket",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Account_IdAccount",
                table: "User",
                column: "IdAccount",
                principalTable: "Account",
                principalColumn: "IdAccount",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_Account_IdAccount",
                table: "Checkin");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkin_Ticket_IdTicket",
                table: "Checkin");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLocation_Event_IdEvent",
                table: "EventLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_EventLocation_Location_IdLocation",
                table: "EventLocation");

            migrationBuilder.DropForeignKey(
                name: "FK_ImageEvent_Event_IdEvent",
                table: "ImageEvent");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_TypeLocation_IdTypeLocation",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_News_Account_IdAccount",
                table: "News");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceTicket_EventLocation_IdEventLocation",
                table: "PriceTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceTicket_TypeTicket_IdTypeTicket",
                table: "PriceTicket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Customer_IdCustomer",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_EventLocation_IdEventLocation",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_TypeTicket_IdTypeTicket",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Account_IdAccount",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_IdAccount",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_IdCustomer",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_IdEventLocation",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_IdTypeTicket",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_PriceTicket_IdEventLocation",
                table: "PriceTicket");

            migrationBuilder.DropIndex(
                name: "IX_PriceTicket_IdTypeTicket",
                table: "PriceTicket");

            migrationBuilder.DropIndex(
                name: "IX_News_IdAccount",
                table: "News");

            migrationBuilder.DropIndex(
                name: "IX_Location_IdTypeLocation",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_ImageEvent_IdEvent",
                table: "ImageEvent");

            migrationBuilder.DropIndex(
                name: "IX_EventLocation_IdEvent",
                table: "EventLocation");

            migrationBuilder.DropIndex(
                name: "IX_EventLocation_IdLocation",
                table: "EventLocation");

            migrationBuilder.DropIndex(
                name: "IX_Checkin_IdAccount",
                table: "Checkin");

            migrationBuilder.DropIndex(
                name: "IX_Checkin_IdTicket",
                table: "Checkin");

            migrationBuilder.AddColumn<int>(
                name: "AccountIdAccount",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerIdCustomer",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventLocationIdEventLocation",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeTicketIdTypeTicket",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventLocationIdEventLocation",
                table: "PriceTicket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeTicketIdTypeTicket",
                table: "PriceTicket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountIdAccount",
                table: "News",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeLocationIdTypeLocation",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventIdEvent",
                table: "ImageEvent",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EventIdEvent",
                table: "EventLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationIdLocation",
                table: "EventLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AccountIdAccount",
                table: "Checkin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TicketIdTicket",
                table: "Checkin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_AccountIdAccount",
                table: "User",
                column: "AccountIdAccount");

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
                name: "IX_PriceTicket_EventLocationIdEventLocation",
                table: "PriceTicket",
                column: "EventLocationIdEventLocation");

            migrationBuilder.CreateIndex(
                name: "IX_PriceTicket_TypeTicketIdTypeTicket",
                table: "PriceTicket",
                column: "TypeTicketIdTypeTicket");

            migrationBuilder.CreateIndex(
                name: "IX_News_AccountIdAccount",
                table: "News",
                column: "AccountIdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Location_TypeLocationIdTypeLocation",
                table: "Location",
                column: "TypeLocationIdTypeLocation");

            migrationBuilder.CreateIndex(
                name: "IX_ImageEvent_EventIdEvent",
                table: "ImageEvent",
                column: "EventIdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_EventIdEvent",
                table: "EventLocation",
                column: "EventIdEvent");

            migrationBuilder.CreateIndex(
                name: "IX_EventLocation_LocationIdLocation",
                table: "EventLocation",
                column: "LocationIdLocation");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_AccountIdAccount",
                table: "Checkin",
                column: "AccountIdAccount");

            migrationBuilder.CreateIndex(
                name: "IX_Checkin_TicketIdTicket",
                table: "Checkin",
                column: "TicketIdTicket");

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_Account_AccountIdAccount",
                table: "Checkin",
                column: "AccountIdAccount",
                principalTable: "Account",
                principalColumn: "IdAccount",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkin_Ticket_TicketIdTicket",
                table: "Checkin",
                column: "TicketIdTicket",
                principalTable: "Ticket",
                principalColumn: "IdTicket",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocation_Event_EventIdEvent",
                table: "EventLocation",
                column: "EventIdEvent",
                principalTable: "Event",
                principalColumn: "IdEvent",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventLocation_Location_LocationIdLocation",
                table: "EventLocation",
                column: "LocationIdLocation",
                principalTable: "Location",
                principalColumn: "IdLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageEvent_Event_EventIdEvent",
                table: "ImageEvent",
                column: "EventIdEvent",
                principalTable: "Event",
                principalColumn: "IdEvent",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_TypeLocation_TypeLocationIdTypeLocation",
                table: "Location",
                column: "TypeLocationIdTypeLocation",
                principalTable: "TypeLocation",
                principalColumn: "IdTypeLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_News_Account_AccountIdAccount",
                table: "News",
                column: "AccountIdAccount",
                principalTable: "Account",
                principalColumn: "IdAccount",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceTicket_EventLocation_EventLocationIdEventLocation",
                table: "PriceTicket",
                column: "EventLocationIdEventLocation",
                principalTable: "EventLocation",
                principalColumn: "IdEventLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceTicket_TypeTicket_TypeTicketIdTypeTicket",
                table: "PriceTicket",
                column: "TypeTicketIdTypeTicket",
                principalTable: "TypeTicket",
                principalColumn: "IdTypeTicket",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Customer_CustomerIdCustomer",
                table: "Ticket",
                column: "CustomerIdCustomer",
                principalTable: "Customer",
                principalColumn: "IdCustomer",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_EventLocation_EventLocationIdEventLocation",
                table: "Ticket",
                column: "EventLocationIdEventLocation",
                principalTable: "EventLocation",
                principalColumn: "IdEventLocation",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_TypeTicket_TypeTicketIdTypeTicket",
                table: "Ticket",
                column: "TypeTicketIdTypeTicket",
                principalTable: "TypeTicket",
                principalColumn: "IdTypeTicket",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Account_AccountIdAccount",
                table: "User",
                column: "AccountIdAccount",
                principalTable: "Account",
                principalColumn: "IdAccount",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
