using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.DbModel;

namespace TerminalDashboard.Services
{
    public class DataService
    {
        private readonly TerminalContext _terminalContext;
        private readonly MyAirport _myAirport;

        public DataService(IServiceProvider serviceProvider, IOptions<MyAirport> MyAirport)
        {
            _terminalContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TerminalContext>();
            _myAirport = MyAirport.Value;
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
        public async Task<Model.FlightSammary> GetFlightSammary(int lastMinutes)
        {
            var dateFrom = DateTime.Now.Subtract(TimeSpan.FromMinutes(lastMinutes));
            int tookOff = await 
                _terminalContext.Flights
                .Include(x => x.From)
                .Where(f =>
                    f.DepartureTime < DateTime.Now && f.DepartureTime > dateFrom && f.From!.Ident == _myAirport.Ident)
                .CountAsync();

            //int tookOff = await (from f in _terminalContext.Flights
            //          where f.DepartureTime < DateTime.Now && f.DepartureTime > DateTime.Now.Subtract(TimeSpan.FromMinutes(lastMinutes)) && f.From!.Ident == _myAirport.Ident
            //          select f).CountAsync();
            //int aboutToLand = await (from f in _terminalContext.Flights
            //          where f.LandingTime > DateTime.Now && f.LandingTime < DateTime.Now.Add(TimeSpan.FromMinutes(lastMinutes)) && f.To!.Ident == _myAirport.Ident
            //                         select f).CountAsync();
            //int atTheAirport = await (from f in _terminalContext.Flights
            //          where f.DepartureTime > DateTime.Now && f.From!.Ident == _myAirport.Ident || f.LandingTime < DateTime.Now && f.To!.Ident == _myAirport.Ident
            //          select f).CountAsync();
            Model.FlightSammary flightSammery = new()
            {
                TookOff = tookOff,
                AboutToLand = 0, //aboutToLand,
                AtTheAirport = 0//atTheAirport
            };
            return flightSammery;
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