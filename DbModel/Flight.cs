using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel
{
    public class Flight
    {
        [Key]
        [MaxLength(10)]
        public string? ID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
        public Guid AirplaneID { get; set; }

        [ForeignKey("AirplaneID")]
        public virtual Airplane Airplane { get; set; } = null!;
        public virtual List<Passenger>? Passengers { get; set; }
    }
}
