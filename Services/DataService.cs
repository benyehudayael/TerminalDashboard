using DAL;
using DbModel;

namespace TerminalDashboard.Services
{
    public class DataService
    {
        private readonly TerminalContext TerminalContext;

        public DataService(TerminalContext TerminalContext)
        {
            this.TerminalContext = TerminalContext;
        }
        public void AddNewAirplane(Airplane airplane)
        {
            TerminalContext.Airplanes.Add(airplane);
            TerminalContext.SaveChanges();
        }

        public void RemoveAirplane()
        {

        }
        public void AddNewFirm(Firm firm)
        {
            TerminalContext.Firms.Add(firm);
            TerminalContext.SaveChanges();
        }

        public void RemoveFirm()
        {

        }
        public void AddNewFlight(Flight flight)
        {
            TerminalContext.Flights.Add(flight);
            TerminalContext.SaveChanges();
        }

        public void RemoveFlight()
        {

        }
        public void AddNewPassenger(Passenger passenger)
        {
            TerminalContext.Passengers.Add(passenger);
            TerminalContext.SaveChanges();
        }

        public void RemovePassenger()
        {

        }
        public void AddNewSuitcase(Suitcase suitcase)
        {
            TerminalContext.Suitcases.Add(suitcase);
            TerminalContext.SaveChanges();
        }

        public void RemoveSuitcase()
        {

        }

    }
}