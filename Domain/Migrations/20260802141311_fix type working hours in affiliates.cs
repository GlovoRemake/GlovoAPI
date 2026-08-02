using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class fixtypeworkinghoursinaffiliates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAffiliates_CompanyAffiliatesWorkingHours_WorkingHour~",
                table: "CompanyAffiliates");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingHoursId",
                table: "CompanyAffiliates",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAffiliates_CompanyAffiliatesWorkingHours_WorkingHour~",
                table: "CompanyAffiliates",
                column: "WorkingHoursId",
                principalTable: "CompanyAffiliatesWorkingHours",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyAffiliates_CompanyAffiliatesWorkingHours_WorkingHour~",
                table: "CompanyAffiliates");

            migrationBuilder.AlterColumn<int>(
                name: "WorkingHoursId",
                table: "CompanyAffiliates",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyAffiliates_CompanyAffiliatesWorkingHours_WorkingHour~",
                table: "CompanyAffiliates",
                column: "WorkingHoursId",
                principalTable: "CompanyAffiliatesWorkingHours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
