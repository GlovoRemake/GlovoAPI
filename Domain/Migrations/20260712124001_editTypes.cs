using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class editTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_CompanyTypes_TypeId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_TypeId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IconPath",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Companies");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "CompanyTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "CompanyTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IconPath = table.Column<string>(type: "text", nullable: false),
                    ParentTypeId = table.Column<int>(type: "integer", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Type_Type_ParentTypeId",
                        column: x => x.ParentTypeId,
                        principalTable: "Type",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypes_CompanyId",
                table: "CompanyTypes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyTypes_TypeId",
                table: "CompanyTypes",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_ParentTypeId",
                table: "Type",
                column: "ParentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypes_Companies_CompanyId",
                table: "CompanyTypes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyTypes_Type_TypeId",
                table: "CompanyTypes",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypes_Companies_CompanyId",
                table: "CompanyTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyTypes_Type_TypeId",
                table: "CompanyTypes");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTypes_CompanyId",
                table: "CompanyTypes");

            migrationBuilder.DropIndex(
                name: "IX_CompanyTypes_TypeId",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "CompanyTypes");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "CompanyTypes");

            migrationBuilder.AddColumn<string>(
                name: "IconPath",
                table: "CompanyTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CompanyTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CompanyTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_TypeId",
                table: "Companies",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_CompanyTypes_TypeId",
                table: "Companies",
                column: "TypeId",
                principalTable: "CompanyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
