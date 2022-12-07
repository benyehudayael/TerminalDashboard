using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel
{
    public class Passenger
    {
        [Key]
        public Guid ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? FlightId { get; set; }

        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; } = null!;
        public virtual List<Suitcase>? Suitcases { get; set; }

    }
}