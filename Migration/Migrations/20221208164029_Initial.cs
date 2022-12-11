using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TerminalDashboard.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Ident = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Elevationft = table.Column<int>(name: "Elevation_ft", type: "int", nullable: false),
                    Continent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Isocountry = table.Column<string>(name: "Iso_country", type: "nvarchar(max)", nullable: true),
                    Isoregion = table.Column<string>(name: "Iso_region", type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gpscode = table.Column<string>(name: "Gps_code", type: "nvarchar(max)", nullable: true),
                    Iatacode = table.Column<string>(name: "Iata_code", type: "nvarchar(max)", nullable: true),
                    Localcode = table.Column<string>(name: "Local_code", type: "nvarchar(max)", nullable: true),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Ident);
                });

            migrationBuilder.CreateTable(
                name: "Firms",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Airplanes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirmID = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airplanes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Airplanes_Firms",
                        column: x => x.FirmID,
                        principalTable: "Firms",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LandingTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    From = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    To = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirplaneID = table.Column<Guid>(type: "uniqueidentifier", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Flights_Airplanes",
                        column: x => x.AirplaneID,
                        principalTable: "Airplanes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Passengers_Flights",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Suitcases",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 200, nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suitcases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Suitcases_Passengers",
                        column: x => x.OwnerId,
                        principalTable: "Passengers",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airplanes_FirmID",
                table: "Airplanes",
                column: "FirmID");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirplaneID",
                table: "Flights",
                column: "AirplaneID");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_FlightId",
                table: "Passengers",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Suitcases_OwnerId",
                table: "Suitcases",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Suitcases");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airplanes");

            migrationBuilder.DropTable(
                name: "Firms");
        }
    }
}
