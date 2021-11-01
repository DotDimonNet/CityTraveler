using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        [Route("get")]
        public IActionResult GetAll()
        {
            return Json(_service.GetAll());
        }

        [HttpGet]
        [Route("getByTitle")]
        public IActionResult GetEntertainmentByTitle(string title)
        {
            return Json(_service.GetEntertainmentByTitle(title));
        }

        [HttpGet]
        [Route("getByAddress")]
        public IActionResult GetEntertainmentByAddress([FromBody] AddressDTO dto)
        {
            return Json(_service.GetEntertainmentByAddress(houseNumber, apartmentNumber, streetTitle));
        }
    }
}