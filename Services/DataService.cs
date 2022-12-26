using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.DbModel;
using TerminalDashboard.Mappers;

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

        public async Task<List<dynamic>> GetLastFlights(int minutes)
        {
            var nowPlus40Min = DateTime.Now.Add(TimeSpan.FromMinutes(minutes));
            var nowMinus40Min = DateTime.Now.Subtract(TimeSpan.FromMinutes(minutes));
            try
            {
                var data = await _terminalContext.Flights
                    .Include(x => x.Airplane)
                    .Include(x => x.FromAirport)
                    .Include(x => x.ToAirport)
                    .Select(x => new { 
                        Flight = x, 
                        IsLanding = x.ToIdent == _myAirport.Ident,
                        Time = x.ToIdent == _myAirport.Ident ? x.LandingTime : x.DepartureTime
                    })
                    .Where(f => 
                        (f.Flight.DepartureTime < nowPlus40Min && 
                        f.Flight.DepartureTime > DateTime.Now.AddMinutes(-1) && 
                        f.Flight.FromIdent == _myAirport.Ident) || 
                        (f.Flight.LandingTime > nowMinus40Min && 
                        f.Flight.LandingTime < DateTime.Now.AddMinutes(1) && 
                        f.Flight.ToIdent == _myAirport.Ident))
                    .OrderByDescending(x => x.Time)
                    .ToListAsync();
                return data.Select(x => (object)new { Flight = Mapper.FlightToModel(x.Flight), x.IsLanding, x.Time}).ToList();
            }
            catch (Exception e) 
            { throw; }
        }
        public async Task<List<Model.Flight>> getflightsummarys()
        {
            var nowPlus40Min = DateTime.Now.Add(TimeSpan.FromMinutes(40));
            var nowMinus20Min = DateTime.Now.Subtract(TimeSpan.FromMinutes(20));
            try
            {
                var data = await _terminalContext.Flights.Include(x => x.Airplane)
                   .Where(f => f.DepartureTime < nowPlus40Min && f.DepartureTime > DateTime.Now && f.FromIdent == _myAirport.Ident || f.LandingTime > nowMinus20Min && f.LandingTime < DateTime.Now && f.ToIdent == _myAirport.Ident)
                   .ToListAsync();
                return data.Select(x => Mapper.FlightToModel(x)).ToList();
            }
            catch (Exception e)
            { throw; }
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
                return await _terminalContext.Flights
                    .Include(x => x.Airplane)
                    .Where(f => f.DepartureTime > DateTime.Now)
                    .ToListAsync();
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

                var id = await query.FirstOrDefaultAsync();
                return id;
            }
            catch (Exception) { 
                throw; 
            }
        }
        public async Task<Model.Summary> GetSummary(int lastMinutes)
        {
            var lastMinutesInTimeSpan = TimeSpan.FromMinutes(lastMinutes);
            var dateFrom = DateTime.Now.Subtract(lastMinutesInTimeSpan);
            var dateTo = DateTime.Now.Add(lastMinutesInTimeSpan);
            var nowPlusTimeToUnLoad = DateTime.Now.Add(TimeSpan.FromMinutes(10));
            var nowMinusTimeToUnLoad = DateTime.Now.Subtract(TimeSpan.FromMinutes(10));
            int tookOff = await 
                _terminalContext.Flights
                    .Include(x => x.FromAirport)
                    .Include(x => x.ToAirport)
                    .Where(f => (lastMinutes == 30 && f.DepartureTime > DateTime.Now) ||
                                (lastMinutes == -30 && f.DepartureTime < DateTime.Now) &&
                                f.DepartureTime < dateTo && f.FromIdent == _myAirport.Ident)
                    .CountAsync();
            int aboutToLand = await
                _terminalContext.Flights
                    .Include(x => x.FromAirport)
                    .Include(x => x.ToAirport)
                    .Where(f => (lastMinutes == 30 && f.LandingTime > DateTime.Now && f.LandingTime < dateTo) ||
                                (lastMinutes == -30 && f.LandingTime < DateTime.Now && f.LandingTime > dateTo)
                                 && f.ToIdent == _myAirport.Ident)
                    .CountAsync();
            int atTheAirport = await 
                _terminalContext.Flights
                    .Include(x => x.FromAirport)
                    .Include(x => x.ToAirport)
                    .Where(f => (
                        (f.DepartureTime > DateTime.Now && f.DepartureTime < dateTo && f.FromIdent == _myAirport.Ident) || 
                        (f.LandingTime < DateTime.Now && f.LandingTime > dateFrom && f.ToIdent == _myAirport.Ident)))
                    .CountAsync();
            int sumOfAirplanes = await
                _terminalContext.Airplanes.CountAsync();
            int passengersInTheAirportArea = await
                _terminalContext.Passengers
                    .Include(x => x.Flight)
                    .Where(p => 
                        (p.Flight.DepartureTime < dateTo && p.Flight.DepartureTime > DateTime.Now) || 
                        (p.Flight.LandingTime < DateTime.Now && p.Flight.LandingTime > dateFrom))
                    .CountAsync();
            int tookOffPassengers = await
                _terminalContext.Passengers
                    .Include(x => x.Flight)
                    .Where(p => p.Flight.DepartureTime < DateTime.Now && p.Flight.DepartureTime > dateFrom && p.Flight.FromIdent == _myAirport.Ident)
                    .CountAsync();
            int landSoonPassengers = await
                 _terminalContext.Passengers
                .Include(x => x.Flight)
                .Where(p =>
                    p.Flight.LandingTime > DateTime.Now && p.Flight.LandingTime < dateTo && p.Flight.ToIdent == _myAirport.Ident)
                .CountAsync();
            int suitcasesWaitingToBeUnloaded = await 
                _terminalContext.Suitcases
                .Include(x => x.Passenger)
                .Where(s => s.Passenger.Flight.LandingTime < DateTime.Now && s.Passenger.Flight.LandingTime > nowPlusTimeToUnLoad)
                .CountAsync();
            int suitcasesOnAConveyorBelt = await
               _terminalContext.Suitcases
                .Include(x => x.Passenger)
                .Where(s => s.Passenger.Flight.LandingTime < nowMinusTimeToUnLoad && s.Passenger.Flight.LandingTime > dateFrom)
                .CountAsync();
            int suitcasesWaitingToBeloaded = await
               _terminalContext.Suitcases
                .Include(x => x.Passenger)
                .Where(s => s.Passenger.Flight.DepartureTime > DateTime.Now && s.Passenger.Flight.DepartureTime < nowPlusTimeToUnLoad)
                .CountAsync();
            Model.Summary Sammery = new()
            {
                TookOff = tookOff,
                AboutToLand = aboutToLand,
                AtTheAirport = atTheAirport,
                SumOfAirplanes = sumOfAirplanes,
                PassengersInTheAirportArea = passengersInTheAirportArea,
                TookOffPassengers = tookOffPassengers,
                LandSoonPassengers = landSoonPassengers,
                SuitcasesWaitingToBeUnloaded = suitcasesWaitingToBeUnloaded,
                SuitcasesOnAConveyorBelt = suitcasesOnAConveyorBelt,
                SuitcasesWaitingToBeloaded = suitcasesWaitingToBeloaded
            };
            return Sammery;
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

        public async Task<List<Flight>> GetFlightsWithPassengers()
        {
            return await _terminalContext.Flights
                    .Include(x => x.Airplane)
                    .Include(x => x.Passengers)
                    .Where(f => f.DepartureTime > DateTime.Now)
                    .ToListAsync();
        }

        public async Task CleanFlights()
        {
            await _terminalContext.Database.ExecuteSqlAsync(
                $@"
                    DELETE FROM [dbo].[Suitcases]
                    DELETE FROM [dbo].[Passengers]
                    DELETE FROM [dbo].[Flights]
                ");
            //var suitcases = await _terminalContext.Suitcases.ToListAsync();
            //_terminalContext.Suitcases.RemoveRange(suitcases);
            //await _terminalContext.SaveChangesAsync();

            //var passengers = await _terminalContext.Passengers.ToListAsync();
            //_terminalContext.Passengers.RemoveRange(passengers);
            //await _terminalContext.SaveChangesAsync();

            //var flights = await _terminalContext.Flights.ToListAsync();
            //_terminalContext.Flights.RemoveRange(flights);
            //await _terminalContext.SaveChangesAsync();
        }
    }
}