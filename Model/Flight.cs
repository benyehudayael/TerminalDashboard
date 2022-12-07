using System;
using System.Collections.Generic;
using System.Linq;
namespace Model
{
    public class Flight
    {
        public string? ID { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime LandingTime { get; set; }
        public Guid From { get; set; }
        public Guid To { get; set; }
        public Guid Firm { get; set; }
    }
}
