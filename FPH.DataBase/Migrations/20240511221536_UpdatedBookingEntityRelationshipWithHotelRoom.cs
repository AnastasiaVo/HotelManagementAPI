using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPH.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookingEntityRelationshipWithHotelRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_RoomId",
                table: "Booking",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_HotelRoom_RoomId",
                table: "Booking",
                column: "RoomId",
                principalTable: "HotelRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_HotelRoom_RoomId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_RoomId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Booking");
        }
    }
}
