using Microsoft.Extensions.Options;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.DbModel;
using TerminalDashboard.Services;

namespace WorkerService
{
    public class DataManipulatorWorker : BackgroundService
    {
        private readonly ILogger<DataManipulatorWorker> _logger;
        private readonly IOptions<MyAirport> _MyAirport;

        private DataService _dataService;
        public DataManipulatorWorker(ILogger<DataManipulatorWorker> logger, IServiceProvider serviceProvider, IOptions<MyAirport> MyAirport)
        {
            _logger = logger;
            _dataService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataService>();
            _MyAirport = MyAirport;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //List<Firm> firms = await _dataService.GetFirms();
                    //Airplane airplane = InstancesHelper.CreateAirplane(firms);
                    //_dataService.AddNewAirplane(airplane);

                    List<Name> names = await _dataService.GetNames();  
                    List<Flight> flights = await _dataService.GetFlightsWithPassengers();
                    Passenger passenger = InstancesHelper.CreatePassenger(flights, names);
                    if (passenger.Age > 0)
                    {
                        _dataService.AddNewPassenger(passenger);

                        Guid OwnerId = passenger.ID; /*await _dataService.GetRandomPassangerId();*/
                        var random = new Random();
                        for (int i = 0; i < random.Next(1, 2); i++)
                        {
                            Suitcase suitcase = InstancesHelper.CreateSuitcase(OwnerId);
                            _dataService.AddNewSuitcase(suitcase);
                        }
                    }

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                catch(Exception e)
                {

                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}