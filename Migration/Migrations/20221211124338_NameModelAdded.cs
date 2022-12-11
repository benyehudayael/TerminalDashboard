using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerminalDashboard.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class NameModelAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Flights");

            migrationBuilder.AddColumn<string>(
                name: "FromIdent",
                table: "Flights",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToIdent",
                table: "Flights",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Name",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Name", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FromIdent",
                table: "Flights",
                column: "FromIdent");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ToIdent",
                table: "Flights",
                column: "ToIdent");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_FromIdent",
                table: "Flights",
                column: "FromIdent",
                principalTable: "Airports",
                principalColumn: "Ident");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Airports_ToIdent",
                table: "Flights",
                column: "ToIdent",
                principalTable: "Airports",
                principalColumn: "Ident");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_FromIdent",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Airports_ToIdent",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Name");

            migrationBuilder.DropIndex(
                name: "IX_Flights_FromIdent",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_ToIdent",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FromIdent",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ToIdent",
                table: "Flights");

            migrationBuilder.AddColumn<Guid>(
                name: "From",
                table: "Flights",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "To",
                table: "Flights",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
