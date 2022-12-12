using DAL;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TerminalDashboard.DbModel;

namespace TerminalDashboard.Services
{
    public class DataService
    {
        private readonly TerminalContext TerminalContext;

        public DataService(TerminalContext TerminalContext)
        {
            this.TerminalContext = TerminalContext;
        }
        public async Task<List<Airplane>> GetAirplanes()
        {
            try
            {
                return await TerminalContext.Airplanes.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Airplane>();
            }
        }
        public void AddNewAirplane(Airplane airplane)
        {
            TerminalContext.Airplanes.Add(airplane);
            TerminalContext.SaveChanges();
        }

        public void RemoveAirplane()
        {

        }
        public async Task<List<Firm>> GetFirms()
        {
            try
            {
                return await TerminalContext.Firms.ToListAsync();
            }
            catch (Exception ex) 
            {
                return new List<Firm>();
            }
        }
        public void AddNewFirm(Firm firm)
        {
            TerminalContext.Firms.Add(firm);
            TerminalContext.SaveChanges();
        }

        public void RemoveFirm()
        {

        }
        public async Task<List<Flight>> GetFlights()
        {
            try
            {
                return await TerminalContext.Flights.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Flight>();
            }
        }
        public void AddNewFlight(Flight flight)
        {
            TerminalContext.Flights.Add(flight);
            TerminalContext.SaveChanges();
        }

        public void RemoveFlight()
        {

        }
        public async Task<List<Airport>> GetAirports()
        {
            try
            {
                return await TerminalContext.Airports.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Airport>();
            }
        }
        public async Task<List<Name>> GetNames()
        {
            try
            {
                return await TerminalContext.Name.ToListAsync();
            }
            catch (Exception)
            {
                return new List<Name>();
            }
        }
        public async Task<Guid> GetRandomPassangerId()
        {
            try
            {
                var query = (from p in TerminalContext.Passengers
                             join f in TerminalContext.Flights on p.FlightId equals f.ID
                             join s in TerminalContext.Suitcases on p.ID equals s.OwnerId into ps
                             from s in ps.DefaultIfEmpty()
                             where f.DepartureTime > DateTime.Now
                             group p by p.ID into g
                             where g.Count() < 2
                             orderby Guid.NewGuid()
                             select new { ID = g.Key, Count = g.Count() }).Distinct().Take(1);
                
                return query.FirstOrDefault().ID;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<Passenger>> GetPassengers()
        {
            try
            {
                return await TerminalContext.Passengers.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Passenger>();
            }
        }
        public void AddNewPassenger(Passenger passenger)
        {
            TerminalContext.Passengers.Add(passenger);
            TerminalContext.SaveChanges();
        }

        public void RemovePassenger()
        {

        }
        public async Task<List<Suitcase>> GetSuitcases()
        {
            try
            {
                return await TerminalContext.Suitcases.ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<Suitcase>();
            }
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