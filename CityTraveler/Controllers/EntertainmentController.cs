using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using CityTraveler.Domain.DTO;
using System.Linq;
using CityTraveler.Domain.Enums;
using System.Threading.Tasks;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/entertainment")]
    public class EntertainmentController : Controller
    {
        private readonly ILogger<EntertainmentController> _logger;
        private readonly IEntertainmentService _service;

        public EntertainmentController(ILogger<EntertainmentController> logger, IEntertainmentService entertainmentService)
        {
            _service = entertainmentService;
            _logger = logger;
        }

        [HttpGet("all")]
        public IActionResult GetAll(EntertainmentType type = EntertainmentType.All)
        {
            return Json(_service.GetAllDTO(type));
        }

        [HttpGet("title")]
        public IActionResult GetEntertainmentByTitle([FromQuery] string title, EntertainmentType type = EntertainmentType.All)
        {
            return Json(_service.GetEntertainmentsDTOByTitle(title, type));
        }

        [HttpGet("ids")]
        public IActionResult GetEntertainments([FromQuery] IEnumerable<Guid> ids, EntertainmentType type = EntertainmentType.All)
        {
            return Json(_service.GetEntertainmentsDTO(ids, type));
        }

        [HttpGet("get-by-street")]
        public IActionResult GetEntertainmentsByStreet([FromQuery] string streetTitle, EntertainmentType type = EntertainmentType.All)
        {
            return Json(_service.GetEntertainmentsDTOByStreet(streetTitle, type));
        }

        [HttpGet("coordinates")]
        public IActionResult GetEntertainmentsByCoordinates([FromQuery] CoordinatesDTO coordinatesDto, EntertainmentType type = EntertainmentType.All)
        {
            return Json(_service.GetEntertainmentsDTOByCoordinates(coordinatesDto, type));
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetEntertainmentById([FromQuery] Guid id, EntertainmentType type = EntertainmentType.All)
        {
            return Json(await _service.GetEntertainmentDTOByIdAsync(id, type));
        }

        [HttpGet("address")]
        public async Task<IActionResult> GetEntertainmentByAddress([FromQuery] AddressGetDTO addressDto, EntertainmentType type = EntertainmentType.All)
        {
            return Json(await _service.GetEntertainmentDTOByAddressAsync(addressDto, type));
        }

        [HttpGet("types")]
        public IActionResult GetTypes()
        {
            return Json();
        }
    }
}