using System;
using System.Collections.Generic;
using System.Linq;
namespace TerminalDashboard.Model
{
    public class Flight
    {
        public string? ID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public Airport? From { get; set; }
        public Airport? To { get; set; }
        public Guid AirplaneID { get; set; }
    }
}
