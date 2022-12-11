using Microsoft.EntityFrameworkCore.Migrations;
using TerminalDashboard.Common;

#nullable disable

namespace TerminalDashboard.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class initiaDataNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "", @".\assets\Names.csv");
            var data = FileHelper.ReadCsvNamesData(dataFilePath);
            //static string normalize(string x) => x.Replace("'", "''");
            data.ForEach(x => migrationBuilder.Sql(
              @$"INSERT INTO [dbo].[Name]([Id],[FirstName],[LastName]) VALUES ('{x.Id}','{x.FirstName}' ,'{x.LastName}')"));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DELETE * FROM [dbo].[Name]");
        }
    }
}
