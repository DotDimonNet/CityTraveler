using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CityTraveler.Domain.Filters;
using CityTraveler.Domain.Filters.Admin;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminPanelService _service;

        public AdminController(ILogger<AdminController> logger, IAdminPanelService adminPanelService)
        {
            _service = adminPanelService;
            _logger = logger;
        }

        [HttpGet("users")]
        public async Task<IActionResult> AdminFilterUsers([FromQuery] FilterAdminUser filter)
        {
            var users = await _service.FilterUsers(filter);
            return Json(users);
        }

        [HttpGet("entertaiments")]
        public async Task<IActionResult> AdminFilterEntertaiments([FromQuery] FilterAdminEntertaiment filter)
        {
            var entertainments = await _service.FilterEntertaiments(filter);
            return Json(entertainments); 
        }

        [HttpGet("trips")]
        public async Task<IActionResult> FilterTrips([FromQuery] FilterAdminTrip filter)
        {
            var trips = await _service.FilterTrips(filter);
            return Json(trips);
        }

        [HttpGet("reviews")]
        public async Task<IActionResult> FilterReview([FromQuery] FilterAdminReview filter)
        {
            var reviews = await _service.FilterReview(filter);
            return Json(reviews);
        }
        [HttpGet("streets")]
        public async Task<IActionResult> FindAdressStreets([FromQuery] FilterAdminStreet filter)
        {
            var addresses = await _service.FindAdressStreets(filter);
            return Json(addresses);
        }
    }
}
