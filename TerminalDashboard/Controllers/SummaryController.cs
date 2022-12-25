using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerminalDashboard.Model;
using TerminalDashboard.Services;

namespace TerminalDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SummaryController : ControllerBase
    {
        private DataService _dataService;
        public SummaryController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<Summary> GetSummary(int lastMinutes)
        {
            try
            {
                var data =  await _dataService.GetSummary(lastMinutes);
                return data;
            }
            catch (Exception e)
            {
                throw;
            }
            
        }
    }
}
