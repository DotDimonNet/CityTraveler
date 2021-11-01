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

        [HttpGet]
        [Route("getById")]
        public async Task<IActionResult> GetTripById(Guid tripId)
        { 
            if (tripId!=null)
            {
                return Json(_service.GetTripById(tripId));
            }
            else
            {
                return new StatusCodeResult(404);
            }     
        }

        [HttpGet]
        [Route("getTripsByName")]
        public async Task<IActionResult> GetTripsName(string tripName)
        {
            if (String.IsNullOrEmpty(tripName))
            {
                return Json(_service.GetTripsByName(tripName));
            }
            else
            {
                return new StatusCodeResult(404);   
            }
        }

        [HttpPost]
        [Route("postNewTrip")]
        public async Task<IActionResult> AddNewTrip(TripModel trip)
        {
            try
            {         
                await _service.AddNewTripAsync(trip);
            }
            catch
            {
                return new StatusCodeResult(500);
            }
            return RedirectToAction();
        }

        [HttpDelete]
        [Route("deleteTrip")]
        public async Task<IActionResult> DeleteTrip(Guid tripId)
        {
            try
            {
                await _service.DeleteTripAsync(tripId);
            }
            catch 
            {
                return new StatusCodeResult(500);     
            }
            return RedirectToAction();
        }

        [HttpPut]
        [Route("addTripToEntertainment")]
        public async Task<IActionResult> AddEntertainmentToTrip(Guid tripId, EntertaimentModel entertainment)
        {
            try
            {
                await _service.AddEntertainmetToTripAsync(tripId, entertainment);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
            return RedirectToAction();
        }
    }
}
