using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCartAdditional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    AffiliateId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCarts_CompanyAffiliates_AffiliateId",
                        column: x => x.AffiliateId,
                        principalTable: "CompanyAffiliates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCarts_CompanyProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CompanyProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCartsAdditionals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CartId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    AdditionalId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCartsAdditionals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCartsAdditionals_Additionals_AdditionalId",
                        column: x => x.AdditionalId,
                        principalTable: "Additionals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCartsAdditionals_CompanyProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "CompanyProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCartsAdditionals_UserCarts_CartId",
                        column: x => x.CartId,
                        principalTable: "UserCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCarts_AffiliateId",
                table: "UserCarts",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCarts_ProductId",
                table: "UserCarts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCarts_UserId",
                table: "UserCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCartsAdditionals_AdditionalId",
                table: "UserCartsAdditionals",
                column: "AdditionalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCartsAdditionals_CartId",
                table: "UserCartsAdditionals",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCartsAdditionals_ProductId",
                table: "UserCartsAdditionals",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCartsAdditionals");

            migrationBuilder.DropTable(
                name: "UserCarts");
        }
    }
}
