using CityTraveler.Domain.DTO;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using System.Collections.Generic;
using System;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/city-architecture")]
    public class CityArchitectureController : Controller
    {
        private readonly ILogger<CityArchitectureController> _logger;
        private readonly ICityArchitectureService _service;
        private readonly IEntertainmentService _entertainmentService;

        public CityArchitectureController(ApplicationContext context, ILogger<CityArchitectureController> logger, ICityArchitectureService cityArchitectureService, IEntertainmentService entertainmentService)
        {
            _entertainmentService = entertainmentService;
            _service = cityArchitectureService;
            _logger = logger;
        }

        [HttpPost("add-entertainment")]
        public async Task<IActionResult> AddEntertainment([FromBody] EntertainmentGetDTO entertainmentDto)
        {
            return Json(await _service.AddEntertainment(entertainmentDto));
        }

        [HttpPost("add-entertainments")]
        public async Task<IActionResult> AddEntertainments([FromBody] IEnumerable<EntertainmentGetDTO> entertainmentsDto)
        {
            return Json(await _service.AddEntertainments(entertainmentsDto));
        }

        [HttpPost("add-street")]
        public async Task<IActionResult> AddStreet([FromBody] StreetGetDTO streetDto)
        {
            return Json(await _service.AddStreet(streetDto));
        }

        [HttpDelete("remove-entertainment")]
        public async Task<IActionResult> RemoveEntertainment([FromQuery] Guid id) 
        {
            return Json(await _service.RemoveEntertainment(id));
        }

        [HttpDelete("remove-street")]
        public async Task<IActionResult> RemoveStreet([FromQuery] Guid id)
        {
            return Json(await _service.RemoveStreet(id));
        }

        [HttpPut("update-entertainment")]
        public async Task<IActionResult> UpdateEntertainment([FromBody] EntertainmentUpdateDTO entertainmentDto)
        {
            return Json(await _service.UpdateEntertainment(entertainmentDto));
        }

        [HttpPut("update-street")]
        public async Task<IActionResult> UpdateStreet([FromBody] StreetDTO streetDto)
        {
            return Json(await _service.UpdateStreet(streetDto));
        }
    }
}
