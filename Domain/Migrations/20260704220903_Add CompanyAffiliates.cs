using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyAffiliates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyAffiliates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    WorkingHoursId = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AverageTimeCookingMin = table.Column<TimeSpan>(type: "interval", nullable: false),
                    AverageTimeCookingMax = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAffiliates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAffiliates_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyAffiliates_CompanyAffiliateLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "CompanyAffiliateLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyAffiliates_CompanyAffiliatesWorkingHours_WorkingHour~",
                        column: x => x.WorkingHoursId,
                        principalTable: "CompanyAffiliatesWorkingHours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAffiliates_CompanyId",
                table: "CompanyAffiliates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAffiliates_LocationId",
                table: "CompanyAffiliates",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAffiliates_WorkingHoursId",
                table: "CompanyAffiliates",
                column: "WorkingHoursId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyAffiliates");
        }
    }
}
