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

        [HttpGet("id")]
        public IActionResult GetTripById(Guid tripId)
        { 
            if (tripId == Guid.Empty)
            {
                return new NotFoundResult();
            }
            else
            {
                return Json(_service.GetTripById(tripId));
            }
        }

        [HttpPost("trip")]
        public async Task<IActionResult> AddNewTrip([FromBody] AddNewTripDTO trip)
        {
            return Json(await _service.AddNewTripAsync(trip));           
        }

        [HttpDelete("trip")]
        public async Task<IActionResult> DeleteTrip([FromQuery] Guid tripId)
        {
            return Json(await _service.DeleteTripAsync(tripId));                
        }

        [HttpPut("entertainment")]
        public async Task<IActionResult> AddEntertainmentToTrip([FromQuery] Guid tripId, [FromBody] EntertainmentGetDTO entertainment)
        {
           return Json(await _service.AddEntertainmetToTripAsync(tripId, entertainment));       
        }

        [HttpDelete("entertainment")]
        public async Task<IActionResult> DeleteEntertainmentFromTrip([FromQuery] Guid tripId, [FromQuery] Guid entertainmentId)
        {
           return Json(await _service.DeleteEntertainmentFromTrip(tripId, entertainmentId));          
        }

        [HttpGet("default-trips")]
        public IActionResult GetDafaultTrips([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return Json(_service.GetDefaultTrips(skip, take));
        }
    
        [HttpGet("default-trip")]
        public IActionResult GetDefaulttripById([FromQuery] Guid defaultTripId)
        {
            return Json(_service.GetDefaultTripById(defaultTripId));
        }

        [HttpPost("default-trip")]
        public async Task<IActionResult> AddDefaultTrip([FromBody] DefaultTripDTO defaultTrip)
        {
           return Json(await _service.AddDefaultTrip(defaultTrip));           
        }

        [HttpPut("trip-title")]
        public async Task<IActionResult> UpdateTripTitle([FromQuery] Guid tripId, [FromQuery] string newtitle) 
        {
           return Json(await _service.UpdateTripTitleAsync(tripId, newtitle));            
        }

        [HttpPut("trip-description")]
        public async Task<IActionResult> UpdateTripDescription([FromQuery] Guid tripId, [FromBody] string newDescription)
        {
          return Json(await _service.UpdateTripDescriptionAsync(tripId, newDescription));
        }
    }
}
