using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using CityTraveler.Domain.Enums;
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

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class StatisticController : Controller {
        private readonly ILogger<CityArchitectureController> _logger;
        private readonly IStatisticService _service;

        public StatisticController(ILogger<CityArchitectureController> logger, IStatisticService adminPanelService)
        {
            _service = adminPanelService;
            _logger = logger;
        }

        [HttpGet("get-users-average-age")]
        public async Task<IActionResult> GetAverageAgeUser()
        {
            return Json(_service.GetAverageAgeUser());
        }

        [HttpGet("get-average-entertaiment-in-trip")]
        public async Task<IActionResult> GetAvarageEnternaimentInTrip()
        {
            return Json(_service.GetAvarageEnternaimentInTrip());
        }

        [HttpGet("get-users-admin")]
        public async Task<IActionResult> GetActivityUserTrip(Guid UserID, DateTime time)
        {
            return (time != null)&&(UserID != null) ?
                Json(_service.GetActivityUserTrip(UserID, time))
                : new NotFoundResult();
        }

        [HttpGet("get-count-passed-trip")]
        public async Task<IActionResult> GetCountPassedUserTrip(Guid UserID)
        {
            return UserID != null ? Json(_service.GetCountPassedUserTrip(UserID)) : new NotFoundResult();
        }

        [HttpGet("get-average-price-trip")]
        public async Task<IActionResult> GetAveragePriceUserTrip(Guid UserID)
        {
            return UserID != null ? Json(_service.GetAveragePriceUserTrip(UserID)) : new NotFoundResult();
        }

        [HttpGet("get-average-time-trip")]
        public async Task<IActionResult> GetAverageTimeUserTrip(Guid UserID)
        {
            return UserID != null ? Json(_service.GetAverageTimeUserTrip(UserID)) : new NotFoundResult();
        }

        [HttpGet("get-user-average-entertaiment-in-trip")]
        public async Task<IActionResult> GetAverageEntertaimentUserTrip(Guid UserID)
        {
            return UserID != null ? Json(_service.GetAverageEntertaimentUserTrip(UserID)) : new NotFoundResult();
        }

        [HttpGet("get-user-average-entertaiment-in-trip")]
        public async Task<IActionResult> GetAverageRatingUserTrip(Guid UserID)
        {
            return UserID != null ? Json(_service.GetAverageRatingUserTrip(UserID)) : new NotFoundResult();
        }

        [HttpGet("get-user-average-entertaiment-in-trip")]
        public async Task<IActionResult> QuantityPassEntertaiment(Guid UserID)
        {
            return UserID != null ? Json(_service.QuantityPassEntertaiment(UserID)) : new NotFoundResult();
        }

        [HttpGet("get-user-average-entertaiment-in-trip")]
        public async Task<IActionResult> QuantityPassEntertaiment(Guid UserID, EntertaimentModel rev)
        {
            return (rev != null) && (UserID != null) ? 
                Json(_service.QuantityPassEntertaiment(UserID))
                : new NotFoundResult();
        }
    }

}
