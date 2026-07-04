using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddCompanyAffiliatesProductsCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyAffiliatesProductsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyAffiliateId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAffiliatesProductsCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAffiliatesProductsCategories_CompanyAffiliates_Compa~",
                        column: x => x.CompanyAffiliateId,
                        principalTable: "CompanyAffiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyAffiliatesProductsCategories_CompanyProductCategorie~",
                        column: x => x.CategoryId,
                        principalTable: "CompanyProductCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAffiliatesProductsCategories_CategoryId",
                table: "CompanyAffiliatesProductsCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAffiliatesProductsCategories_CompanyAffiliateId",
                table: "CompanyAffiliatesProductsCategories",
                column: "CompanyAffiliateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyAffiliatesProductsCategories");
        }
    }
}
