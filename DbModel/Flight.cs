using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerminalDashboard.DbModel
{
    public class Flight
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(10)]
        public string? NumberId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public string? FromIdent { get; set; }
        public string? ToIdent { get; set; }

        [ForeignKey("FromIdent")]
        public Airport? FromAirport { get; set; }
        [ForeignKey("ToIdent")]
        public Airport? ToAirport { get; set; }

        public Guid AirplaneID { get; set; }

        [ForeignKey("AirplaneID")]
        public virtual Airplane Airplane { get; set; } = null!;
        public virtual List<Passenger>? Passengers { get; set; }
    }
}
