using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class changeaffiliateIdtocompanyIdinUserCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCarts_CompanyAffiliates_AffiliateId",
                table: "UserCarts");

            migrationBuilder.RenameColumn(
                name: "AffiliateId",
                table: "UserCarts",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCarts_AffiliateId",
                table: "UserCarts",
                newName: "IX_UserCarts_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCarts_Companies_CompanyId",
                table: "UserCarts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCarts_Companies_CompanyId",
                table: "UserCarts");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "UserCarts",
                newName: "AffiliateId");

            migrationBuilder.RenameIndex(
                name: "IX_UserCarts_CompanyId",
                table: "UserCarts",
                newName: "IX_UserCarts_AffiliateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCarts_CompanyAffiliates_AffiliateId",
                table: "UserCarts",
                column: "AffiliateId",
                principalTable: "CompanyAffiliates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
