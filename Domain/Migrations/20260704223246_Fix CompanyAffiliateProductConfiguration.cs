using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixCompanyAffiliateProductConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyAffiliateId",
                table: "CompanyAffiliatesProducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAffiliatesProducts_CompanyAffiliateId",
                table: "CompanyAffiliatesProducts",
                column: "CompanyAffiliateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAffiliatesProducts_CompanyAffiliates_CompanyAffiliat~",
                table: "CompanyAffiliatesProducts",
                column: "CompanyAffiliateId",
                principalTable: "CompanyAffiliates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAffiliatesProducts_CompanyAffiliates_CompanyAffiliat~",
                table: "CompanyAffiliatesProducts");

            migrationBuilder.DropIndex(
                name: "IX_CompanyAffiliatesProducts_CompanyAffiliateId",
                table: "CompanyAffiliatesProducts");

            migrationBuilder.DropColumn(
                name: "CompanyAffiliateId",
                table: "CompanyAffiliatesProducts");
        }
    }
}
