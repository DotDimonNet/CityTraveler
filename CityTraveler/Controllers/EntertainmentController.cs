using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using CityTraveler.Domain.DTO;
using System.Linq;
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

        [HttpGet("get")]
        public IActionResult GetAll()
        {
            return Json(_service.GetAllDTO());
        }

        [HttpGet("get-by-title")]
        public IActionResult GetEntertainmentByTitle([FromQuery] string title)
        {
            return Json(_service.GetEntertainmentsDTOByTitle(title));
        }

        [HttpGet("get-by-ids")]
        public IActionResult GetEntertainments([FromQuery] IEnumerable<Guid> ids)
        {
            return Json(_service.GetEntertainmentsDTO(ids));
        }

        [HttpGet("get-by-street")]
        public IActionResult GetEntertainmentsByStreet([FromQuery] string streetTitle)
        {
            return Json(_service.GetEntertainmentsDTOByStreet(streetTitle));
        }

        [HttpGet("get-by-coordinates")]
        public IActionResult GetEntertainmentsByCoordinates([FromQuery] CoordinatesDTO coordinatesDto)
        {
            return Json(_service.GetEntertainmentsDTOByCoordinates(coordinatesDto));
        }

        [HttpGet("get-by-id")]
        public IActionResult GetEntertainmentById([FromQuery] Guid id)
        {
            return Json(_service.GetEntertainmentDTOById(id));
        }

        [HttpGet("get-by-address")]
        public IActionResult GetEntertainmentByAddress([FromQuery] AddressGetDTO addressDto)
        {
            return Json(_service.GetEntertainmentDTOByAddress(addressDto));
        }
    }
}