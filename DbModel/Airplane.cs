using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbModel
{
    public class Airplane
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? FirmID { get; set; }
        public int TotalSeats { get; set; }

        [ForeignKey("FirmID")]
        public virtual Firm Firm { get; set; } = null!;
        public virtual List<Flight>? Flights { get; set; }
    }
}
