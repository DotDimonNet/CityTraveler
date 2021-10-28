using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private readonly ILogger<TripController> _logger;
        private readonly ITripService _service;

        public TripController(ILogger<TripController> logger, ITripService tripService)
        {
            _service = tripService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetTrips(int skip = 0, int take = 10)
        {
            return Json(_service.GetTrips(skip, take));
        }
    }
}
