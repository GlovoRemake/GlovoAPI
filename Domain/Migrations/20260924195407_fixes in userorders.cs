using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class fixesinuserorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CourierId",
                table: "UserOrders",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "AffiliateId",
                table: "UserOrders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "UserOrders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_AffiliateId",
                table: "UserOrders",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_CompanyId",
                table: "UserOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_CourierId",
                table: "UserOrders",
                column: "CourierId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_AspNetUsers_CourierId",
                table: "UserOrders",
                column: "CourierId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_Companies_CompanyId",
                table: "UserOrders",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserOrders_CompanyAffiliates_AffiliateId",
                table: "UserOrders",
                column: "AffiliateId",
                principalTable: "CompanyAffiliates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_AspNetUsers_CourierId",
                table: "UserOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_Companies_CompanyId",
                table: "UserOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_UserOrders_CompanyAffiliates_AffiliateId",
                table: "UserOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserOrders_AffiliateId",
                table: "UserOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserOrders_CompanyId",
                table: "UserOrders");

            migrationBuilder.DropIndex(
                name: "IX_UserOrders_CourierId",
                table: "UserOrders");

            migrationBuilder.DropColumn(
                name: "AffiliateId",
                table: "UserOrders");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserOrders");

            migrationBuilder.AlterColumn<Guid>(
                name: "CourierId",
                table: "UserOrders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}
