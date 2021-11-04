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
        public IActionResult GetAll(int typeId = 0)
        {
            return Json(_service.GetAllDTO(typeId));
        }

        [HttpGet("get-by-title")]
        public IActionResult GetEntertainmentByTitle([FromQuery] string title, int typeId = 0)
        {
            return Json(_service.GetEntertainmentsDTOByTitle(title, typeId));
        }

        [HttpGet("get-by-ids")]
        public IActionResult GetEntertainments([FromQuery] IEnumerable<Guid> ids, int typeId = 0)
        {
            return Json(_service.GetEntertainmentsDTO(ids, typeId));
        }

        [HttpGet("get-by-street")]
        public IActionResult GetEntertainmentsByStreet([FromQuery] string streetTitle, int typeId = 0)
        {
            return Json(_service.GetEntertainmentsDTOByStreet(streetTitle, typeId));
        }

        [HttpGet("get-by-coordinates")]
        public IActionResult GetEntertainmentsByCoordinates([FromQuery] CoordinatesDTO coordinatesDto, int typeId = 0)
        {
            return Json(_service.GetEntertainmentsDTOByCoordinates(coordinatesDto, typeId));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetEntertainmentById([FromQuery] Guid id, int typeId = 0)
        {
            return Json(await _service.GetEntertainmentDTOById(id, typeId));
        }

        [HttpGet("get-by-address")]
        public async Task<IActionResult> GetEntertainmentByAddress([FromQuery] AddressGetDTO addressDto, int typeId = 0)
        {
            return Json(await _service.GetEntertainmentDTOByAddress(addressDto, typeId));
        }
    }
}