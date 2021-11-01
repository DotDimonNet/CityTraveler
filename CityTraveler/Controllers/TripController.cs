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
            TripModel trip;
            try
            {         
                trip = _service.GetTripById(tripId);
            }
            catch (TripControllerException e) when (tripId == null)
            {

                throw new TripControllerException($"Trip with Id={tripId} not found", e);
            }
            catch (TripControllerException e)
            {
                throw new TripControllerException("Exception on finding trip by Id", e);
            }
            return (Json(trip));
     
        }

        [HttpGet]
        [Route("getTripsByName")]
        public async Task<IActionResult> GetTripsName(string tripName)
        {
            IEnumerable<TripModel> trips;
            try
            {
                trips = _service.GetTripsByName(tripName);
            }
            catch (TripControllerException e) when (tripName == null)
            {

                throw new TripControllerException($"Trip with Tirle={tripName} not found");
            }
            catch (TripControllerException e)
            {

                throw new TripControllerException("Exception on finding trip by Title", e);
            }
            return Json(trips);
        }
        
    }
}
