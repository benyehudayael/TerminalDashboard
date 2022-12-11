using Microsoft.EntityFrameworkCore.Migrations;
using TerminalDashboard.Common;

#nullable disable

namespace TerminalDashboard.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class initialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", @".\assets\airlines.csv");
            var data = FileHelper.ReadCsvFirmData(dataFilePath);
            static string normalize(string x) => x.Replace("'", "''");
            data.ForEach(x => migrationBuilder.Sql(@$"INSERT INTO [dbo].[Firms]([ID],[Name]) VALUES ('{normalize(x.ID)}' ,'{normalize(x.Name)}')"));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DELETE * FROM [dbo].[Firms]");
        }
    }
}
