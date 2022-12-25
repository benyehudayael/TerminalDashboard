using Microsoft.Extensions.Options;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.DbModel;
using TerminalDashboard.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WorkerService
{
    internal class FlightsCreator : BackgroundService
    {
        private readonly ILogger<DataManipulatorWorker> _logger;
        private readonly IOptions<MyAirport> _MyAirport;

        private DataService _dataService;
        public FlightsCreator(ILogger<DataManipulatorWorker> logger, IServiceProvider serviceProvider, IOptions<MyAirport> MyAirport)
        {
            _logger = logger;
            _dataService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<DataService>();
            _MyAirport = MyAirport;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _dataService.CleanFlights();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    List<Airplane> airplanes = await _dataService.GetAirplanes();
                    List<Airport> airports = await _dataService.GetAirports();
                    List<Flight> flights =  await _dataService.GetFlights();
                    var date = (flights.Count > 0 ? flights.Max(x => x.DepartureTime) : DateTime.Now).AddSeconds(30);
                    var todate = DateTime.Now.AddDays(7);
                    if (date < todate) 
                    {
                        do
                        {
                            int sumTries = 0;
                            try
                            {
                                if (sumTries > 2)
                                    break;
                                Flight flight = InstancesHelper.CreateFlight(_MyAirport.Value.Ident.ToString(), airplanes, airports, date, flights);
                                _dataService.AddNewFlight(flight);
                            }
                            catch(Exception e3)
                            {
                                sumTries++;
                                _logger.LogError($"FlightsCreator failed at: {DateTimeOffset.Now} with exception {e3.Message}");
                                continue;
                            }
                            sumTries = 0;
                            date = date.AddSeconds(30);
                        }
                        while ( date < todate );
                    }
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                catch (Exception e)
                {

                }

                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);
            }
        }
    }
}
