using Microsoft.Extensions.Options;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.DbModel;
using TerminalDashboard.Services;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<MyAirport> _MyAirport;

        private DataService _dataService;
        public Worker(ILogger<Worker> logger, DataService dataService, IOptions<MyAirport> MyAirport)
        {
            _logger = logger;
            _dataService = dataService;
            _MyAirport = MyAirport;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Firm> firms = await _dataService.GetFirms();
                List<Airplane> airplanes = await _dataService.GetAirplanes();
                List<Airport> airports = await _dataService.GetAirports();
                List<Flight> flights = await _dataService.GetFlights();
                List<Name> names = await _dataService.GetNames();
                Guid OwnerId = await _dataService.GetRandomPassangerId();

                InstancesHelper.CreateAirplane(firms);
                InstancesHelper.CreateFlight(_MyAirport.Value.Ident.ToString(), airplanes, airports);
                InstancesHelper.CreatePassenger(flights, names);
                InstancesHelper.CreateSuitcase(OwnerId);


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);                

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}