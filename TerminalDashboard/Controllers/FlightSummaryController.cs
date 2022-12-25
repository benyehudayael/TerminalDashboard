using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TerminalDashboard.Model;
using TerminalDashboard.Services;

namespace TerminalDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightSummaryController : ControllerBase
    {
    }
    //[HttpGet("summary")]
    //public async task<list<Model.FlightSummary>> getflightsummarys()
    //{
    //    try
    //    {
    //        var data = await _dataservice.getflightsummarys();
    //        return data;
    //    }
    //    catch (exception e)
    //    {
    //        throw;
    //    }

    //}
}
