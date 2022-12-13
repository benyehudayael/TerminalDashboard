using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TerminalDashboard.DbModel;

namespace TerminalDashboard.Services
{
    public class DataService
    {
        private readonly TerminalContext _terminalContext;

        public DataService(IServiceProvider serviceProvider)
        {
            _terminalContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TerminalContext>();
        }

        // ----- Get ----- //
        public async Task<List<Airplane>> GetAirplanes()
        {
            try
            {
                return await _terminalContext.Airplanes.ToListAsync();
            }
            catch (Exception) { throw; }
        }
        public async Task<List<Firm>> GetFirms()
        {
            try
            {
                return await _terminalContext.Firms.ToListAsync();
            }
            catch (Exception) { throw; }
        }
        
        public async Task<List<Flight>> GetFlights()
        {
            try
            {
                return await _terminalContext.Flights.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Airport>> GetAirports()
        {
            try
            {
                return await _terminalContext.Airports.ToListAsync();
            }
            catch (Exception) { throw; }
        }
        public async Task<List<Name>> GetNames()
        {
            try
            {
                return await _terminalContext.Name.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Name>();
            }
        }
        public async Task<List<Passenger>> GetPassengers()
        {
            try
            {
                return await _terminalContext.Passengers.ToListAsync();
            }
            catch (Exception) { throw; }
        }
        public async Task<List<Suitcase>> GetSuitcases()
        {
            try
            {
                return await _terminalContext.Suitcases.ToListAsync();
            }
            catch (Exception) { throw; }
        }
        // ---- Get specific column ---- //
        public async Task<Guid> GetRandomPassangerId()
        {
            try
            {
                var query = from p in _terminalContext.Passengers
                            join f in _terminalContext.Flights on p.FlightId equals f.ID
                            join s in _terminalContext.Suitcases on p.ID equals s.OwnerId into ps
                            from s in ps.DefaultIfEmpty()
                            where f.DepartureTime > DateTime.Now
                            orderby Guid.NewGuid()
                            group p by p.ID into g
                            where g.Count() < 2
                            select g.Key;

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception) { throw; }
        }
        //---- Add ----//
        public void AddNewPassenger(Passenger passenger)
        {
            _terminalContext.Passengers.Add(passenger);
            _terminalContext.SaveChanges();
        }
        public void AddNewAirplane(Airplane airplane)
        {
            _terminalContext.Airplanes.Add(airplane);
            _terminalContext.SaveChanges();
        }
        public void AddNewFirm(Firm firm)
        {
            _terminalContext.Firms.Add(firm);
            _terminalContext.SaveChanges();
        }
        public void AddNewFlight(Flight flight)
        {
            try
            {
                _terminalContext.Flights.Add(flight);
                _terminalContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AddNewSuitcase(Suitcase suitcase)
        {
            _terminalContext.Suitcases.Add(suitcase);
            _terminalContext.SaveChanges();
        }
        //---- Remove ----//
        public void RemoveFirm()
        {

        }
        public void RemoveAirplane()
        {

        }
        public void RemoveFlight()
        {

        }

        public void RemovePassenger()
        {

        }
        public void RemoveSuitcase()
        {

        }

    }
}