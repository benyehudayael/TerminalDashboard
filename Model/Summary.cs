using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDashboard.Model
{
    public class Summary
    {
        public int AboutToTakeOff { get; set; }
        public int AboutToLand { get; set; }
        public int AtTheAirport { get; set; }
        public int SumOfAirplanes { get; set; }
        public int PassengersInTheAirportArea { get; set; }
        public int PassengersAboutToTakeOff { get; set; }
        public int LandedPassengers { get; set; }
        public int SuitcasesWaitingToBeUnloaded { get; set; }
        public int SuitcasesOnAConveyorBelt { get; set; }
        public int SuitcasesWaitingToBeloaded { get; set; }
    }
}
