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
    [Route("api/info")]
    public class InfoController : Controller
    {
        private readonly ILogger<InfoController> _logger;
        private readonly IInfoService _service;

        public InfoController(ILogger<InfoController> logger, IInfoService infoService)
        {
            _service = infoService;
            _logger = logger;
        }

        [HttpGet("user/popular-entertaiment")]
        public async Task<IActionResult> GetMostPopularEntertaimentInTrips([FromQuery] Guid userId)
        {
            var entertaiment = await _service.GetMostPopularEntertaimentInTrips(userId);
            return Json(entertaiment);
        }

        [HttpGet("trip-popular")]
        public async Task<IActionResult> GetTripByMaxChoiceOfUsers()
        {
            var trip = await _service.GetTripByMaxChoiceOfUsers();
            return Json(trip);
        }

        [HttpGet("review-maxcomment")]
        public async Task<IActionResult> GetReviewByMaxComment([FromQuery] Guid userId)
        {
            var review = await _service.GetReviewByMaxComments(userId);
            return Json(review);
        }

        [HttpGet("trip-maxreview")]
        public async Task<IActionResult> GetTripByMaxReview([FromQuery] Guid userId)
        {
            var trip = await _service.GetTripByMaxReview(userId);
            return Json(trip);
        }

        [HttpGet("trips-lastperiod")]
        public IActionResult GetLastTripBYPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var trip = _service.GetLastTripsByPeriod(start, end);
            return Json(trip);
        }

        [HttpGet("trips-lowprice")]
        public IActionResult GetTripsByLowPrice([FromQuery] int count)
        {
            var trip = _service.GetTripsByLowPrice(count);
            return Json(trip);
        }

        [HttpGet("user-registered")]
        public async Task<IActionResult> GetRegisteredUsersByPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var registeredUsers = await _service.GetRegisteredUsersByPeriod(start, end);
            return Json(registeredUsers);
        }

        [HttpGet("trip-template")]
        public IActionResult GetMostlyUsedTemplate([FromQuery] int count)
        {
            var template = _service.GetMostlyUsedTemplates(count);
            return Json(template);
        }

        [HttpGet("users-create-trip-byperiod")]
        public IActionResult GetUsersCountTripsDateRang([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var usersCount = _service.GetUsersCountTripsDateRange(start, end);
            return Json(usersCount);
        }

        [HttpGet("trip-longest")]
        public async Task<IActionResult> GetLongestTrip()
        {
            var trip = await _service.GetLongestTrip();
            return Json(trip);
        }

        [HttpGet("trip-shotest")]
        public async Task<IActionResult> GetShortestTrip()
        {
            var trip = await _service.GetShortestTrip();
            return Json(trip);
        }

        [HttpGet("trip-created-period")]
        public IActionResult GetTripsCreatedByPeriod([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var tripsCount = _service.GetTripsCreatedByPeriod(start, end);
            return Json(tripsCount);
        }







    }
}
