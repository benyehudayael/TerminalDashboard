using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerminalDashboard.Model;
using TerminalDashboard.Services;

namespace TerminalDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightSammaryController : ControllerBase
    {
        private DataService _dataService;
        public FlightSammaryController(DataService dataService)
        {
            _dataService = dataService;
        }
        [HttpGet]
        public async Task<FlightSammary> GetFlightSammary(int lastMinutes)
        {
            try
            {
                var data =  await _dataService.GetFlightSammary(lastMinutes);
                return data;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
    }
}
