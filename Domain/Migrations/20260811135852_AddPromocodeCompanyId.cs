using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddPromocodeCompanyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "Promocodes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Promocodes_CompanyId",
                table: "Promocodes",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Promocodes_Companies_CompanyId",
                table: "Promocodes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Promocodes_Companies_CompanyId",
                table: "Promocodes");

            migrationBuilder.DropIndex(
                name: "IX_Promocodes_CompanyId",
                table: "Promocodes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Promocodes");
        }
    }
}
