using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDashboard.Model
{
    internal class FlightSummary
    {
        public string? FlightNumber { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
    }
}
