using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDashboard.Model
{
    public class Summary
    {
        public int TookOff { get; set; }
        public int AboutToLand { get; set; }
        public int AtTheAirport { get; set; }
        public int SumOfAirplanes { get; set; }
        public int PassengersInTheAirportArea { get; set; }
        public int TookOffPassengers { get; set; }
        public int LandSoonPassengers { get; set; }
        public int SuitcasesWaitingToBeUnloaded { get; set; }
        public int SuitcasesOnAConveyorBelt { get; set; }
        public int SuitcasesWaitingToBeloaded { get; set; }
    }
}
