using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    TotalPrice = table.Column<double>(type: "double precision", nullable: false),
                    ProductsPrice = table.Column<double>(type: "double precision", nullable: false),
                    DeliveryFee = table.Column<double>(type: "double precision", nullable: false),
                    Fee = table.Column<double>(type: "double precision", nullable: false),
                    TipPercent = table.Column<double>(type: "double precision", nullable: false),
                    TipAmount = table.Column<double>(type: "double precision", nullable: false),
                    PromocodeId = table.Column<int>(type: "integer", nullable: false),
                    Scheduled = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    AnotherReceiverName = table.Column<string>(type: "text", nullable: true),
                    AnotherReceiverPhone = table.Column<string>(type: "text", nullable: true),
                    DeliveryStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CourierId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOrders_UserLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "UserLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_LocationId",
                table: "UserOrders",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOrders_UserId",
                table: "UserOrders",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOrders");
        }
    }
}
