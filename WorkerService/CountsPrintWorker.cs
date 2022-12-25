using DAL;

namespace WorkerService
{
    public class CountsPrintWorker : BackgroundService
    {
        private readonly ILogger<CountsPrintWorker> _logger;
        private TerminalContext _TerminalContext;
        public CountsPrintWorker(ILogger<CountsPrintWorker> logger, IServiceProvider serviceProvider)
        {
            _TerminalContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TerminalContext>();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var AirplaneCount = _TerminalContext.Airplanes.Count();
                var FlightsCount = _TerminalContext.Flights.Count();
                var SuitcasesCount = _TerminalContext.Suitcases.Count();
                var PassengersCount = _TerminalContext.Passengers.Count();

                string airplane = "Total Airplanes:";
                string flight = "Total Flights:";
                string suitcase = "Total Suitcases:";
                string passenger = "Total Passengers:";

                Console.WriteLine(airplane.PadRight(20, '_') + AirplaneCount);
                Console.WriteLine(flight.PadRight(20, '_') + FlightsCount);
                Console.WriteLine(suitcase.PadRight(20, '_') + SuitcasesCount);
                Console.WriteLine(passenger.PadRight(20, '_') + PassengersCount);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}