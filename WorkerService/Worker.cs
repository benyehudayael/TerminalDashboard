using DAL;
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
        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IOptions<MyAirport> MyAirport)
        {
            _logger = logger;
            _dataService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataService>();
            _MyAirport = MyAirport;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                List<Firm> firms = await _dataService.GetFirms();
                Airplane airplane = InstancesHelper.CreateAirplane(firms);
                _dataService.AddNewAirplane(airplane);

                List<Airplane> airplanes = await _dataService.GetAirplanes();
                List<Airport> airports = await _dataService.GetAirports();
                Flight flight = InstancesHelper.CreateFlight(_MyAirport.Value.Ident.ToString(), airplanes, airports);
                _dataService.AddNewFlight(flight);

                List<Flight> flights = await _dataService.GetFlights();
                List<Name> names = await _dataService.GetNames();
                Passenger passenger = InstancesHelper.CreatePassenger(flights, names);
                _dataService.AddNewPassenger(passenger);

                Guid OwnerId = await _dataService.GetRandomPassangerId();
                Suitcase suitcase = InstancesHelper.CreateSuitcase(OwnerId);
                _dataService.AddNewSuitcase(suitcase);


                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);                

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}