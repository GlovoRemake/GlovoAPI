using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class fix_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyAffiliateId",
                table: "Employees",
                column: "CompanyAffiliateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_CompanyAffiliates_CompanyAffiliateId",
                table: "Employees",
                column: "CompanyAffiliateId",
                principalTable: "CompanyAffiliates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_CompanyAffiliates_CompanyAffiliateId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CompanyAffiliateId",
                table: "Employees");
        }
    }
}
