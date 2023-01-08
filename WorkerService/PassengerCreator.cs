using Microsoft.Extensions.Options;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.DbModel;
using TerminalDashboard.Services;

namespace WorkerService
{
    public class PassengerCreator : BackgroundService
    {
        private readonly ILogger<PassengerCreator> _logger;
        private readonly IOptions<MyAirport> _MyAirport;

        private DataService _dataService;
        public PassengerCreator(ILogger<PassengerCreator> logger, IServiceProvider serviceProvider, IOptions<MyAirport> MyAirport)
        {
            _logger = logger;
            _dataService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataService>();
            _MyAirport = MyAirport;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<Name> names = await _dataService.GetNames();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    List<Flight> flights = await _dataService.GetFlightsWithPassengers();
                    var relevantFlights = flights.Where(x => x.Airplane.TotalSeats > x.Passengers?.Count).ToList();
                    if (relevantFlights.Count > 0)
                    {
                        Passenger passenger = InstancesHelper.CreatePassenger(relevantFlights, names);

                        _dataService.AddNewPassenger(passenger);

                        Guid OwnerId = passenger.ID;
                        var random = new Random();
                        for (int i = 0; i < random.Next(1, 2); i++)
                        {
                            Suitcase suitcase = InstancesHelper.CreateSuitcase(OwnerId);
                            _dataService.AddNewSuitcase(suitcase);
                        }

                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                        await Task.Delay(1000, stoppingToken);
                    }
                    else
                        await Task.Delay(60000, stoppingToken);
                }
                catch(Exception e)
                {
                    _logger.LogError("Worker failed at: {time} with exception {exception}", DateTimeOffset.Now, e);
                }
            }
        }
    }
}