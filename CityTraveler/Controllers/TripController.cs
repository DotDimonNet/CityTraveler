using CityTraveler.Repository.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ILogger<TripController> _logger;

        public TripController(ILogger<TripController> logger, IDbContext context)
        {
            
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var conn = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=CityTraveler;Integrated Security=SSPI;";
            var syncManager = new DbSyncManager(conn);
            var context = new DbContext(conn, syncManager);
            await context.InitializeContext();
            return new JsonResult(1);
        }
    }
}
