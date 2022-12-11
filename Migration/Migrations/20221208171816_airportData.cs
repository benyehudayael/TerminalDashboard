using Microsoft.EntityFrameworkCore.Migrations;
using TerminalDashboard.Common;

#nullable disable

namespace TerminalDashboard.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class airportData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", @".\assets\Airports.csv");
            var data = FileHelper.ReadCsvAirportData(dataFilePath);
            static string normalize(string x) => x.Replace("'", "''");
            data.ForEach(x => migrationBuilder.Sql(
              @$"INSERT INTO [dbo].[Airports]([Ident],[Type],[Name],[Elevation_ft],[Continent],[Iso_country],[Iso_region],[Municipality],[Gps_code],[Iata_code],[Local_code],[Coordinates]) VALUES ('{normalize(x.Ident)}' ,'{normalize(x.Type)}' ,'{normalize(x.Name)}' ,'{normalize(x.Elevation_ft)}' ,'{normalize(x.Continent)}' ,'{normalize(x.Iso_country)}' ,'{normalize(x.Iso_region)}' ,'{normalize(x.Municipality)}' ,'{normalize(x.Gps_code)}' ,'{normalize(x.Iata_code)}' ,'{normalize(x.Local_code)}' ,'{normalize(x.Coordinates)}')"));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
              @$"DELETE * FROM [dbo].[Airports]");
        }
    }
}
