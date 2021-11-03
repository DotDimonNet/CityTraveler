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
    public class SearchServiceController
    {
        [ApiController]
        [Route("api/search")]
        public class SocialMediaController : Controller
        {
            private readonly ILogger<SearchServiceController> _logger;
            private readonly ISearchService _service;

            public SocialMediaController(ILogger<SearchServiceController> logger, ISearchService searchService)
            {
                _service = searchService;
                _logger = logger;
            }

            [HttpGet("users")]
            public async Task<IActionResult> FilterUsers(FilterUsers user)
            {
                IEnumerable<ApplicationUserModel> users =await _service.FilterUsers(user);
                return (Json(users));
            }

            [HttpGet("trips")]
            public async Task<IActionResult> FilterTrips(FilterTrips trip)
            {
                IEnumerable<TripModel> trips = _service.FilterTrips(trip);
                return (Json(trips));
            }

            [HttpGet("entertainments")]
            public async Task<IActionResult> FilterEntertainments(FilterEntertainment entertainment)
            {
                IEnumerable<EntertaimentModel> entertainments = _service.FilterEntertainments(entertainment);
                return (Json(entertainments));
            }
        }
    }
}
