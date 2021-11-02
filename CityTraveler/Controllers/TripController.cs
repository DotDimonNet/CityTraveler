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
        public async Task<IActionResult> GetTripById(Guid tripId)
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
        public async Task<IActionResult> AddNewTrip(AddNewTripDTO trip)
        {
            await _service.AddNewTripAsync(trip);
            return RedirectToAction();
        }

        [HttpDelete("trip")]
        public async Task<IActionResult> DeleteTrip(Guid tripId)
        {      
            await _service.DeleteTripAsync(tripId);
            return RedirectToAction();                  
        }
/*
        [HttpPut("entertainment-to-trip")]
        public async Task<IActionResult> AddEntertainmentToTrip([FromBody]Guid tripId, EntertainmentDTO entertainment)
        {      
            await _service.AddEntertainmetToTripAsync(tripId, entertainment);
            return RedirectToAction();
        }*/

        [HttpDelete("entertainment-from-trip")]
        public async Task<IActionResult> DeleteEntertainmentFromTrip([FromQuery] Guid tripID, [FromQuery]Guid entertainmentId)
        {
            await _service.DeleteEntertainmentFromTrip(tripID, entertainmentId);
            return RedirectToAction();
        }
        [HttpGet ("default-trips")]
        public async Task<IActionResult> GetDafaultTrips([FromQuery] int skip=0, [FromQuery] int take=10 )
        {
            return Json(_service.GetDefaultTrips(skip, take));
        }
    }
}
