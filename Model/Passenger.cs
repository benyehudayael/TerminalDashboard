namespace TerminalDashboard.Model
{
    public class Passenger
    {
        public Guid ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public string? FlightId { get; set; }
    }
}