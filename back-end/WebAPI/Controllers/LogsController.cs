using Services;
using DTOs.Input;
using DTOs.Output;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Logs controller
    /// </summary>
    [Route("api/[controller]")]
    public class LogsController : Controller
    {
        private ILogsServices LogsServices { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logsServices">Injected logs services</param>
        public LogsController(ILogsServices logsServices)
        {
            LogsServices = logsServices;
        }

        // POST api/logs
        [HttpPost]
        public async Task<ActionResult<LogOutputDTO>> Post([FromBody] LogDTO log) =>
            Ok(await LogsServices.Create(log: log));

        // GET: api/logs
        [HttpGet]
        public async Task<ActionResult<List<LogOutputDTO>>> Get([FromQuery] PaginationDTO pagination) =>
            Ok(await LogsServices.Get(pagination: pagination));
    }
}

