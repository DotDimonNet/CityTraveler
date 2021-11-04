using CityTraveler.Domain.DTO;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CityTraveler.Services;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using System.Collections.Generic;
using System;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/map")]
    public class MapController : Controller
    {
        private readonly ILogger<MapController> _logger;
        private readonly IMapService _service;

        public MapController(ApplicationContext context, ILogger<MapController> logger, IMapService mapService)
        {
            _service = mapService;
            _logger = logger;
        }

        [HttpGet("street-by-coordinates")]
        public async Task<IActionResult> FindStreetDTOByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return Json(await _service.FindStreetDTOByCoordinates(coordinatesDto));
        }
        
        [HttpGet("address-by-coordinates")]
        public IActionResult FindAddressDTOByCoordinates(CoordinatesDTO coordinatesDto)
        {
            return Json(_service.FindAddressesDTOByCoordinates(coordinatesDto));
        }
        
        [HttpGet("street-by-title")]
        public async Task<IActionResult> FindStreetsDTOByTitle(string streetTitle)
        {
            return Json(await _service.FindStreetsDTOByTitle(streetTitle));
        }
        
        [HttpGet("streets")]
        public IActionResult FindStreets(int skip = 0, int take = 10)
        {
            return Json(_service.FindStreetsDTO(skip, take));
        }
    }
}

