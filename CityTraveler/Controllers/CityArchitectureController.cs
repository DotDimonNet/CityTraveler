using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/cityarchitecture")]
    public class CityArchitectureController : Controller
    {
        private readonly ILogger<CityArchitectureController> _logger;
        private readonly ICityArchitectureService _service;

        public CityArchitectureController(ILogger<CityArchitectureController> logger, ICityArchitectureService cityArchitectureService)
        {
            _service = cityArchitectureService;
            _logger = logger;
        }

        [HttpPut]
        [Route("addEntertainment")]
        public async Task<IActionResult> AddEntertainment(double latitude, double longitude, string houseNumber, string apartmentNumber, double averagePrice, string title, string description)
        {
            var dto = new EntertainmentDTO()
            {
                Address = new AddressModel()
                {
                    Coordinates = new CoordinatesModel() { Latitude = latitude, Longitude = longitude },
                    HouseNumber = houseNumber,
                    ApartmentNumber = apartmentNumber,
                    Street = new StreetModel()
                },
                AveragePrice = new EntertaimentPriceModel() { Title = "Average price", Value = averagePrice},
                Title = title,
                Description = description
            };
            return Json(await _service.AddEntertainment(dto));
        }
    }
}
