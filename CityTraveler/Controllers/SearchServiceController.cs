using CityTraveler.Domain.Entities;
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
    [Route("api/search")]
    public class SearchServiceController : Controller
    {
        private readonly ILogger<SearchServiceController> _logger;
        private readonly ISearchService _service;

        public SearchServiceController(ILogger<SearchServiceController> logger, ISearchService searchService)
        {
            _service = searchService;
            _logger = logger;
        }

        [HttpGet("users")]
        public async Task<IActionResult> FilterUsers([FromQuery] FilterUsers user)
        {
            IEnumerable<ApplicationUserModel> users = await _service.FilterUsers(user);
            return (Json(users));
        }

        [HttpGet("trips")]
        public IActionResult FilterTrips([FromQuery] FilterTrips trip)
        {
            IEnumerable<TripModel> trips = _service.FilterTrips(trip);
            return (Json(trips));
        }

        [HttpGet("entertainments")]
        public ActionResult FilterEntertainments([FromQuery] FilterEntertainment entertainment)
        {
            IEnumerable<EntertaimentModel> entertainments = _service.FilterEntertainments(entertainment);
            return (Json(entertainments));
        }
    }
}
