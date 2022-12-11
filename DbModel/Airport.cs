using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDashboard.DbModel
{
    public class Airport
    {
        [Key]
        public string? Ident { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Elevation_ft { get; set; }
        public string? Continent { get; set; }
        public string? Iso_country { get; set; }
        public string? Iso_region { get; set; }
        public string? Municipality { get; set; }
        public string? Gps_code { get; set; }
        public string? Iata_code { get; set; }
        public string? Local_code { get; set; }
        public string? Coordinates { get; set; }
    }
}
