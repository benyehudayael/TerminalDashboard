using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TerminalDashboard.DbModel
{
    public class Suitcase
    {
        [Key]
        public Guid ID { get; set; }
        public string? Color { get; set; }
        public Guid OwnerId { get; set; }
        public int Size { get; set; }
        public float Weight { get; set; }

        [ForeignKey("OwnerId")]
        public virtual Passenger Passenger { get; set; } = null!;
    }
}
