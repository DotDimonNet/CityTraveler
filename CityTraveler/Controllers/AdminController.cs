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
using CityTraveler.Services;
using CityTraveler.Domain.Filters;
using CityTraveler.Domain.Filters.Admin;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminPanelService _service;

        public AdminController(ILogger<AdminController> logger, IAdminPanelService adminPanelService)
        {
            _service = adminPanelService;
            _logger = logger;
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> AdminFilterUsers([FromQuery] FilterAdminUser filter)
        {
            return Json(_service.FilterUsers(filter));
        }

        [HttpGet("get-entertaiments")]
        public async Task<IActionResult> AdminFilterEntertaiments([FromQuery] FilterAdminEntertaiment filter)
        {
            return Json(_service.FilterEntertaiments(filter)); 
        }

        [HttpGet("get-trips")]
        public async Task<IActionResult> FilterTrips([FromQuery] FilterAdminTrip filter)
        {
            return Json(_service.FilterTrips(filter));
        }

        [HttpGet("get-reviews")]
        public async Task<IActionResult> FilterReview([FromQuery] FilterAdminReview filter)
        {
            return  Json(_service.FilterReview(filter));
        }
        [HttpGet("get-streets")]
        public async Task<IActionResult> FindAdressStreets([FromQuery] FilterAdminStreet filter)
        {
            return Json(_service.FindAdressStreets(filter));
        }
    }
}
