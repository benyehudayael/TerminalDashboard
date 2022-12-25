using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerminalDashboard.Model;
using TerminalDashboard.Services;

namespace TerminalDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private DataService _dataService;
        public FlightController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<List<dynamic>> GetFlights(int lastMinutes)
        {
            try
            {
                var data = await _dataService.GetLastFlights(lastMinutes);
                return data;
            }
            catch (Exception e)
            {
                throw;
            }

        }
        //[HttpGet()]
        //public async Task<List<dynamic>> GetFlights(int lastMinutes)
        //{
        //    try
        //    {
        //        var data = await _dataService.GetLastFlights(lastMinutes);
        //        return data;
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }

        //}

    }
}
