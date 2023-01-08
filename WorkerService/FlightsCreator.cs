using Microsoft.Extensions.Options;
using TerminalDashboard.Common.Configuration;
using TerminalDashboard.DbModel;
using TerminalDashboard.Services;

namespace WorkerService
{
    internal class FlightsCreator : BackgroundService
    {
        private readonly ILogger<PassengerCreator> _logger;
        private readonly IOptions<MyAirport> _MyAirport;

        private DataService _dataService;
        public FlightsCreator(ILogger<PassengerCreator> logger, IServiceProvider serviceProvider, IOptions<MyAirport> MyAirport)
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
                    var date = (flights.Count > 0 ? flights.Max(x => x.DepartureTime) : DateTime.Now).AddSeconds(15);
                    var todate = DateTime.Now.AddHours(3);
                    if (date < todate) 
                    {
                        do
                        {
                            int sumTries = 0;
                            Flight flight = InstancesHelper.CreateFlight(_MyAirport.Value.Ident.ToString(), airplanes, airports, date, flights);
                            try
                            {
                                if (sumTries > 2)
                                    break;
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

                await Task.Delay(1000 * 60 * 60 * 2, stoppingToken);
            }
        }
    }
}
