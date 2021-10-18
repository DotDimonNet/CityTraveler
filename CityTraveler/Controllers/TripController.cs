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
    [Route("api/trip")]
    public class TripController : Controller
    {
        private readonly ILogger<TripController> _logger;

        public TripController(ILogger<TripController> logger)
        {
            
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(1);
        }
    }
}
