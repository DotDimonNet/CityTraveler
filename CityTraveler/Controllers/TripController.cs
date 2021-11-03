using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Domain.Errors;
using CityTraveler.Domain.Entities;
using System.Web;
using CityTraveler.Domain.DTO;

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

        [HttpGet("get")]
        public IActionResult GetTrips([FromQuery] double rating, [FromQuery] TimeSpan optimalSpent,
            [FromQuery] double price, [FromQuery] string tag,  
            [FromQuery] int skip = 0, [FromQuery] int take = 10, [FromQuery] string title = "")
        {
            return Json(_service.GetTrips(title, rating, optimalSpent, price, tag, skip, take));
        }

        [HttpGet("get-by-id")]
        public IActionResult GetTripById(Guid tripId)
        { 
            if (tripId == Guid.Empty)
            {
                return Json(_service.GetTripById(tripId));
            }
            else
            {
                return new NotFoundResult();
            }     
        }

        [HttpPost("trip")]
        public async Task<IActionResult> AddNewTrip([FromBody] AddNewTripDTO trip)
        {
            await _service.AddNewTripAsync(trip);
            return RedirectToAction();
        }

        [HttpDelete("trip")]
        public async Task<IActionResult> DeleteTrip([FromQuery] Guid tripId)
        {      
            await _service.DeleteTripAsync(tripId);
            return RedirectToAction();                  
        }

        [HttpPut("entertainment-to-trip")]
        public async Task<IActionResult> AddEntertainmentToTrip([FromQuery] Guid tripId, [FromBody] EntertainmentDTO entertainment)
        {      
            await _service.AddEntertainmetToTripAsync(tripId, entertainment);
            return RedirectToAction();
        }

        [HttpDelete("entertainment-from-trip")]
        public async Task<IActionResult> DeleteEntertainmentFromTrip([FromQuery] Guid tripId, [FromQuery] Guid entertainmentId)
        {
            await _service.DeleteEntertainmentFromTrip(tripId, entertainmentId);
            return RedirectToAction();
        }

        [HttpGet("default-trips")]
        public IActionResult GetDafaultTrips([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return Json(_service.GetDefaultTrips(skip, take));
        }

        [HttpPost("default-trip")]
        public async Task<IActionResult> AddDefaultTrip([FromBody] DefaultTripDTO defaultTrip)
        {
            await _service.AddDefaultTrip(defaultTrip);
            return RedirectToAction();
        }

        [HttpPut("trip-title")]
        public async Task<IActionResult> UpdateTripTitle([FromQuery] Guid tripId, [FromBody] string newtitle) 
        {
            await _service.UpdateTripTitleAsync(tripId, newtitle);
            return RedirectToAction();
        }

        [HttpPut("trip-description")]
        public async Task<IActionResult> UpdateTripDescription([FromQuery] Guid tripId, [FromBody] string newDescription)
        {
            await _service.UpdateTripDescriptionAsync(tripId, newDescription);
            return RedirectToAction();
        }
    }
}
