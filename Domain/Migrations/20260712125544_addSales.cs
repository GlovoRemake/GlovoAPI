using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class addSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypes_Type_TypeId",
                table: "CompanyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Type_Type_ParentTypeId",
                table: "Type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Type",
                table: "Type");

            migrationBuilder.RenameTable(
                name: "Type",
                newName: "Types");

            migrationBuilder.RenameIndex(
                name: "IX_Type_ParentTypeId",
                table: "Types",
                newName: "IX_Types_ParentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Types",
                table: "Types",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AdditionalSale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NewPrice = table.Column<double>(type: "double precision", nullable: false),
                    PercentSale = table.Column<double>(type: "double precision", nullable: false),
                    AdditionalId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalSale_Additionals_AdditionalId",
                        column: x => x.AdditionalId,
                        principalTable: "Additionals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NewPrice = table.Column<double>(type: "double precision", nullable: false),
                    PercentSale = table.Column<double>(type: "double precision", nullable: false),
                    CompanyProductId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSale_CompanyProducts_CompanyProductId",
                        column: x => x.CompanyProductId,
                        principalTable: "CompanyProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalSale_AdditionalId",
                table: "AdditionalSale",
                column: "AdditionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSale_CompanyProductId",
                table: "ProductSale",
                column: "CompanyProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypes_Types_TypeId",
                table: "CompanyTypes",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Types_Types_ParentTypeId",
                table: "Types",
                column: "ParentTypeId",
                principalTable: "Types",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypes_Types_TypeId",
                table: "CompanyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Types_Types_ParentTypeId",
                table: "Types");

            migrationBuilder.DropTable(
                name: "AdditionalSale");

            migrationBuilder.DropTable(
                name: "ProductSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Types",
                table: "Types");

            migrationBuilder.RenameTable(
                name: "Types",
                newName: "Type");

            migrationBuilder.RenameIndex(
                name: "IX_Types_ParentTypeId",
                table: "Type",
                newName: "IX_Type_ParentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Type",
                table: "Type",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypes_Type_TypeId",
                table: "CompanyTypes",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Type_Type_ParentTypeId",
                table: "Type",
                column: "ParentTypeId",
                principalTable: "Type",
                principalColumn: "Id");
        }
    }
}
