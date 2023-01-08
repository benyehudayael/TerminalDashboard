using TerminalDashboard.DbModel;
using TerminalDashboard.Model;

namespace TerminalDashboard.Mappers
{
    public class Mapper
    {
        public static DbModel.Airplane AirplaneFromModel(TerminalDashboard.Model.Airplane airplane)
        {
            return new DbModel.Airplane()
            {
                ID= airplane.ID,
                FirmID= airplane.FirmID,
                TotalSeats= airplane.TotalSeats
            };
        }
        public static Model.Airplane AirplaneToModel(DbModel.Airplane airplane)
        {
            return new Model.Airplane()
            {
                ID = airplane.ID,
                FirmID = airplane.FirmID,
                TotalSeats = airplane.TotalSeats
            };
        }
        public static DbModel.Flight FlightFromModel(Model.Flight flight) => new DbModel.Flight()
        {
            Id = flight.Id,
            NumberId= flight.NumberId,
            DepartureTime = flight.DepartureTime,
            LandingTime = flight.LandingTime,
            FromIdent = flight.FromIdent,
            ToIdent = flight.ToIdent,
            AirplaneID = flight.AirplaneID
        };
        public static Model.Flight FlightToModel(DbModel.Flight flight)
        {
            return new Model.Flight()
            {
                Id = flight.Id,
                NumberId= flight.NumberId,
                DepartureTime = flight.DepartureTime,
                LandingTime = flight.LandingTime,
                FromIdent = flight.FromIdent,
                ToIdent = flight.ToIdent,
                AirplaneID = flight.AirplaneID,
                Airplane = Mapper.AirplaneToModel(flight.Airplane)
            };
        }
        public static DbModel.Airport AirportFromModel(Model.Airport airport)
        {
            return new DbModel.Airport()
            {
                Ident = airport.Ident,
                Continent= airport.Continent,
                Elevation_ft= airport.Elevation_ft,
                Gps_code= airport.Gps_code,
                Coordinates= airport.Coordinates,
                Local_code= airport.Local_code,
                Iata_code= airport.Iata_code,
                Iso_country= airport.Iso_country,
                Iso_region= airport.Iso_region,
                Municipality= airport.Municipality,
                Name= airport.Name,
                Type= airport.Type,
            };
        }
        public static Model.Airport AirportToModel(DbModel.Airport airport)
        {
            return new Model.Airport()                                          
            {
                Ident = airport.Ident,
                Continent = airport.Continent,
                Elevation_ft = airport.Elevation_ft,
                Gps_code = airport.Gps_code,
                Coordinates = airport.Coordinates,
                Local_code = airport.Local_code,
                Iata_code = airport.Iata_code,
                Iso_country = airport.Iso_country,
                Iso_region = airport.Iso_region,
                Municipality = airport.Municipality,
                Name = airport.Name,
                Type = airport.Type,
            };
        }
        public static DbModel.Firm FirmFromModel(Model.Firm firm)
        {
            return new DbModel.Firm()
            {
                ID = firm.ID,
                Name= firm.Name
            };
        }
        public static Model.Firm FirmToModel(DbModel.Firm firm)
        {
            return new Model.Firm()
            {
                ID = firm.ID,
                Name = firm.Name
            };
        }
        public static DbModel.Passenger PassengerFromModel(Model.Passenger passenger)
        {
            return new DbModel.Passenger()
            {
                ID = passenger.ID,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Age = passenger.Age,
                FlightId = passenger.FlightId
            };
        }
        public static Model.Passenger PassengerToModel(DbModel.Passenger passenger)
        {
            return new Model.Passenger()
            {
                ID = passenger.ID,
                FirstName = passenger.FirstName,
                LastName = passenger.LastName,
                Age = passenger.Age,
                FlightId = passenger.FlightId
            };
        }
        public static DbModel.Suitcase SuitcaseFromModel(Model.Suitcase suitcase)
        {
            return new DbModel.Suitcase()
            {
                ID = suitcase.ID,
                Color= suitcase.Color,
                OwnerId= suitcase.OwnerId,
                Size = suitcase.Size,
                Weight= suitcase.Weight
            };
        }
        public static Model.Suitcase SuitcaseToModel(DbModel.Suitcase suitcase)
        {
            return new Model.Suitcase()
            {
                ID = suitcase.ID,
                Color = suitcase.Color,
                OwnerId = suitcase.OwnerId,
                Size = suitcase.Size,
                Weight = suitcase.Weight
            };
        }


    }
}