using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixPromocodeIdFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_PromocodeId",
                table: "UserOrders",
                column: "PromocodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_Promocodes_PromocodeId",
                table: "UserOrders",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_Promocodes_PromocodeId",
                table: "UserOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserOrders_PromocodeId",
                table: "UserOrders");
        }
    }
}
