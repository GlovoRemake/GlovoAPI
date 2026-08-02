using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class fixuserlocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "UserLocations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserLocations_CityId",
                table: "UserLocations",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLocations_Cities_CityId",
                table: "UserLocations",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLocations_Cities_CityId",
                table: "UserLocations");

            migrationBuilder.DropIndex(
                name: "IX_UserLocations_CityId",
                table: "UserLocations");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "UserLocations");
        }
    }
}
