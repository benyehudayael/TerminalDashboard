using System.ComponentModel.DataAnnotations;

namespace TerminalDashboard.DbModel
{
    public class Firm
    {
        [Key]
        public string? ID { get; set; }
        public string? Name { get; set; }

        public virtual List<Airplane>? Airplanes { get; set; }
    }
}
