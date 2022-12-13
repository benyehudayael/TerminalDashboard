using GeoCoordinatePortable;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using TerminalDashboard.DbModel;

namespace WorkerService
{
    public class InstancesHelper
    {
        public static Airplane CreateAirplane(List<Firm> firms)
        {
            string? FirmID = firms[new Random().Next(0, firms.Count)].ID;
            int TotalSeats = new Random().Next(70, 400);

            Airplane airplane = new()
            {
                ID = Guid.NewGuid(),
                FirmID = FirmID,
                TotalSeats = TotalSeats
            };
            return airplane;
        }
        public static Flight CreateFlight(string myAirportID, List<Airplane> airplanes, List<Airport> airports)
        {
            DateTime DepartureTime = GetRandomDateTime(DateTime.Now, DateTime.Now.AddHours(12));
            Guid AirplaneID = airplanes[new Random().Next(airplanes.Count)].ID;

            Flight flight = new Flight();
            flight.ID = CreateFlightId();
            flight.AirplaneID = AirplaneID;
            flight.DepartureTime = DepartureTime;

            if (new Random().Next(0, 100) % 2 == 0) {
                flight.From = airports.Find(x => x.Ident == myAirportID);
                flight.To = airports[new Random().Next(airports.Count)];
            }
            else
            {
                flight.From = airports[new Random().Next(airports.Count)];
                flight.To = airports.Find(x => x.Ident == myAirportID);
            }
            long flightDurationInMinutes = CalcFlightDuration(CalcDistanceBetweenAirports(flight.From, flight.To));
            DateTime LandingTime = DepartureTime + new TimeSpan(flightDurationInMinutes * 600000000);

            flight.LandingTime = LandingTime;
            return flight;
        }

        public static Passenger CreatePassenger(List<Flight> flights, List<Name> names)
        {
            Passenger passenger = new()
            {
                ID = Guid.NewGuid(),
                FirstName = names[new Random().Next(0, names.Count)].FirstName,
                LastName = names[new Random().Next(0, names.Count)].LastName,
                Age = new Random().Next(1, 100),
                FlightId = flights[new Random().Next(0, flights.Count)].ID
            };
            return passenger;
        }
        public static Suitcase CreateSuitcase(Guid OwnerID)
        {
            Suitcase suitcase = new()
            {
                ID = Guid.NewGuid(),
                Color = Color.FromArgb(new Random().Next(256), new Random().Next(256), new Random().Next(256)).ToString(),
                OwnerId = OwnerID,
                Size = new Random().Next(1000),//Check this!!!
                Weight = (float)new Random().NextDouble() * (400 - 50) + 50,
            };
            return suitcase;
        }
        public static string CreateFlightId()
        {
            var sb = new StringBuilder();
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            sb.Append(letters[new Random().Next(letters.Length)]);
            sb.Append(letters[new Random().Next(letters.Length)]);
            sb.Append(RandomNumberGenerator.GetInt32(100, 999));
            return sb.ToString();
        }

        private static long CalcFlightDuration(double distance)
        {
            var kmPerMinute = 740.0 / 60;
            return (long)(distance / 1000 / kmPerMinute);
        }

        public static double CalcDistanceBetweenAirports(Airport airport1, Airport airport2)
        {
            double lat = Convert.ToDouble(airport1?.Coordinates?.Split(",")[0]);
            double lng = Convert.ToDouble(airport1?.Coordinates?.Split(",")[1]);
            double lat1 = Convert.ToDouble(airport2?.Coordinates?.Split(",")[0]);
            double lng1 = Convert.ToDouble(airport2?.Coordinates?.Split(",")[1]);
            var sCoord = new GeoCoordinate(lat, lng);
            var eCoord = new GeoCoordinate(lat1, lng1);

            return sCoord.GetDistanceTo(eCoord);
        }

        private static DateTime GetRandomDateTime(DateTime startDate, DateTime endDate)
        {
            TimeSpan timeSpan = endDate - startDate;
            TimeSpan newSpan = new(0, new Random().Next(0, (int)timeSpan.TotalMinutes), 0);
            return startDate + newSpan;
        }
    }
}
