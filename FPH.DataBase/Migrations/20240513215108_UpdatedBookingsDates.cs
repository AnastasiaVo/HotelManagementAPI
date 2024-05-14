using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPH.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookingsDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckInDateString",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CheckOutDateString",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInDateString",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CheckOutDateString",
                table: "Booking");
        }
    }
}
