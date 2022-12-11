using CsvHelper;
using CsvHelper.Configuration;
using TerminalDashboard.DbModel;
using System.Globalization;

namespace TerminalDashboard.Common
{
    public class FileHelper
    {
        public static List<dynamic> ReadCsvData(string filePath)
        {
            using var reader = new StreamReader(filePath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };
            using var csv = new CsvReader(reader, config);
            var data = csv.GetRecords<dynamic>().ToList();
            return data;
        }

        public static List<Firm> ReadCsvFirmData(string filePath)
        {
            var data = ReadCsvData(filePath);
            return data.Select(x => new Firm() { ID = x.Field1, Name = x.Field2 }).ToList();
        }

        public static List<Airport> ReadCsvAirportData(string filePath)
        {
            var data = ReadCsvData(filePath);
            return data.Select(x => 
            new Airport() { 
                Ident = x.ident,
                Type = x.type, 
                Name = x.name, 
                Elevation_ft = x.elevation_ft, 
                Continent = x.continent, 
                Iso_country = x.iso_country, 
                Iso_region = x.iso_region, 
                Municipality = x.municipality, 
                Gps_code = x.gps_code, 
                Iata_code = x.iata_code, 
                Local_code = x.local_code, 
                Coordinates = x.coordinates
            }).ToList();
        }
        public static List<Name> ReadCsvNamesData(string filePath)
        {
            var data = ReadCsvData(filePath);
            return data.Select(x => new Name() { Id = Guid.NewGuid(), LastName = x.last, FirstName = x.first }).ToList();
        }

    }
}