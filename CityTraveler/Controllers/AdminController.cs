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

        [HttpGet("get-users-admin")]
        public async Task<IActionResult> AdminFilterUsers(FilterAdminUser filter)
        {
            return filter != null ? Json(_service.AdminFilterUsers(filter)) : new NotFoundResult();
        }

        [HttpGet("get-entertaiments-admin")]
        public async Task<IActionResult> AdminFilterEntertaiments(FilterAdminEntertaiment filter)
        {
            return filter != null ?  Json(_service.AdminFilterEntertaiments(filter)) : new NotFoundResult(); 
        }

        [HttpGet("get-trips-admin")]
        public async Task<IActionResult> AdminFilterTrips(FilterAdminTrip filter)
        {
            return filter != null ? Json(_service.AdminFilterTrips(filter)) : new NotFoundResult();
        }

        [HttpGet("get-reviews-admin")]
        public async Task<IActionResult> AdminFilterReview(FilterAdminReview filter)
        {
            return filter != null ? Json(_service.AdminFilterReview(filter)) : new NotFoundResult();
        }
        [HttpGet("get-streets-admin")]
        public async Task<IActionResult> AdminFindAdressStreets(FilterAdminStreet filter)
        {
            return  filter != null ? Json(_service.AdminFindAdressStreets(filter)) : new NotFoundResult();
        }
    }
}
