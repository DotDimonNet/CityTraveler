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
    [Route("api/statistic")]
    public class StatisticController : Controller
    {
        private readonly ILogger<StatisticController> _logger;
        private readonly IStatisticService _service;

        public StatisticController(ILogger<StatisticController> logger, IStatisticService adminPanelService)
        {
            _service = adminPanelService;
            _logger = logger;
        }

        [HttpGet("get-users-average-age")]
        public async Task<IActionResult> GetAverageAgeUser()
        {
            return  Json(await _service.GetAverageAgeUser());
        }

        [HttpGet("get-average-entertaiment-in-trip")]
        public async Task<IActionResult> GetAvarageEnternaimentInTrip()
        {
            return Json( await _service.GetAvarageEnternaimentInTrip());
        }

        [HttpGet("get-users-admin")]
        public async Task<IActionResult> GetActivityUserTrip(Guid userId, DateTime timeStart, DateTime timeEnd)
        {
            return (timeEnd != null) && (timeStart != null) ?
                Json(_service.GetActivityUserTrip(userId, timeStart,timeEnd))
                : new NotFoundResult();
        }

        [HttpGet("get-count-passed-trip")]
        public async Task<IActionResult> GetCountPassedUserTrip(Guid userId)
        {
            return Json(_service.GetCountPassedUserTrip(userId));
        }

        [HttpGet("get-average-price-trip")]
        public async Task<IActionResult> GetAveragePriceUserTrip(Guid userId)
        {
            return Json(_service.GetAveragePriceUserTrip(userId));
        }

        [HttpGet("get-max-time-trip")]
        public async Task<IActionResult> GetMaxTimeUserTrip(Guid userId)
        {
            return Json(_service.GetMaxTimeUserTrip(userId));
        }

        [HttpGet("get-min-time-trip")]
        public async Task<IActionResult> GetMinTimeUserTrip(Guid userId)
        {
            return Json(_service.GetMinTimeUserTrip(userId));
        }

        [HttpGet("get-user-average-entertaiment-in-trip")]
        public async Task<IActionResult> GetAverageEntertaimentUserTrip(Guid userId)
        {
            return Json(_service.GetAverageEntertaimentUserTrip(userId));
        }

        [HttpGet("get-user-average-rating-in-trip")]
        public async Task<IActionResult> GetAverageRatingUserTrip(Guid userId)
        {
            return Json(_service.GetAverageRatingUserTrip(userId));
        }

        [HttpGet("get-user-pass-entertaiment")]
        public async Task<IActionResult> QuantityPassEntertaiment(Guid userId)
        {
            return Json(_service.QuantityPassEntertaiment(userId));
        }

        [HttpGet("get-user-trip-with-entertaiment")]
        public async Task<IActionResult> GetTripVisitEntertaiment(Guid userId, EntertaimentModel entertaiment)
        {
            return entertaiment != null ?
                Json(_service.GetTripVisitEntertaiment(userId, entertaiment))
                : new NotFoundResult();
        }
        [HttpGet("get-user-average-rating-reviews")]
        public async Task<IActionResult> GetAverageUserReviewRating(Guid userId)
        {
            return Json(_service.GetAverageUserReviewRating(userId));
        }
    }

}
