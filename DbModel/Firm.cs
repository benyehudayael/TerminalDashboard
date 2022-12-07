using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbModel
{
    public class Firm
    {
        [Key]
        public Guid ID { get; set; }
        public string? Name { get; set; }

        public virtual List<Airplane>? Airplanes { get; set; }
    }
}
