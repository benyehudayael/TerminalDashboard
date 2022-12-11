using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalDashboard.Model
{
    public class Suitcase
    {
        public Guid ID { get; set; }
        public string? Color { get; set; }
        public Guid OwnerId { get; set; }
        public int Size { get; set; }
        public float Weight { get; set; }
    }
}
