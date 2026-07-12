using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class editNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalSale_Additionals_AdditionalId",
                table: "AdditionalSale");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSale_CompanyProducts_CompanyProductId",
                table: "ProductSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSale",
                table: "ProductSale");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdditionalSale",
                table: "AdditionalSale");

            migrationBuilder.RenameTable(
                name: "ProductSale",
                newName: "ProductSales");

            migrationBuilder.RenameTable(
                name: "AdditionalSale",
                newName: "AdditionalSales");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSale_CompanyProductId",
                table: "ProductSales",
                newName: "IX_ProductSales_CompanyProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalSale_AdditionalId",
                table: "AdditionalSales",
                newName: "IX_AdditionalSales_AdditionalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSales",
                table: "ProductSales",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalSales",
                table: "AdditionalSales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalSales_Additionals_AdditionalId",
                table: "AdditionalSales",
                column: "AdditionalId",
                principalTable: "Additionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSales_CompanyProducts_CompanyProductId",
                table: "ProductSales",
                column: "CompanyProductId",
                principalTable: "CompanyProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalSales_Additionals_AdditionalId",
                table: "AdditionalSales");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSales_CompanyProducts_CompanyProductId",
                table: "ProductSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductSales",
                table: "ProductSales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdditionalSales",
                table: "AdditionalSales");

            migrationBuilder.RenameTable(
                name: "ProductSales",
                newName: "ProductSale");

            migrationBuilder.RenameTable(
                name: "AdditionalSales",
                newName: "AdditionalSale");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSales_CompanyProductId",
                table: "ProductSale",
                newName: "IX_ProductSale_CompanyProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AdditionalSales_AdditionalId",
                table: "AdditionalSale",
                newName: "IX_AdditionalSale_AdditionalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductSale",
                table: "ProductSale",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdditionalSale",
                table: "AdditionalSale",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalSale_Additionals_AdditionalId",
                table: "AdditionalSale",
                column: "AdditionalId",
                principalTable: "Additionals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSale_CompanyProducts_CompanyProductId",
                table: "ProductSale",
                column: "CompanyProductId",
                principalTable: "CompanyProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
