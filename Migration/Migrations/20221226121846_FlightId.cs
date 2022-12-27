using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerminalDashboard.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class FlightId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Flights",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Flights",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "FlightId",
                table: "Passengers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Flights",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "NumberId",
                table: "Flights",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flights",
                table: "Flights",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Flights",
                table: "Passengers",
                column: "FlightId", 
                principalTable: "Flights",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Flights",
                table: "Passengers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flights",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "NumberId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Flights",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "FlightId",
                table: "Passengers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ID",
                table: "Flights",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
               name: "PK_Flights",
               table: "Flights",
               column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Flights",
                table: "Passengers",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "ID");
        }
    }
}
