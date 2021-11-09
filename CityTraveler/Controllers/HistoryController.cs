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
    [Route("api/history")]
    public class HistoryController : Controller
    {
        private readonly ILogger<HistoryController> _logger;
        private readonly IHistoryService _service;
        public HistoryController(ILogger<HistoryController> logger, IHistoryService adminPanelService)
        {
            _service = adminPanelService;
            _logger = logger;
        }

        [HttpGet("get-user-last-comment")]
        public async Task<IActionResult> GetUserLastComment(Guid userId)
        {
            return Json(_service.GetUserLastComment(userId));
        }
        [HttpGet("get-last-comment")]
        public async Task<IActionResult> GetLastComment()
        {
            return Json(_service.GetLastComment());
        }

        [HttpGet("get-last-review")]
        public async Task<IActionResult> GetLastReview()
        {
            return Json(_service.GetLastReview());
        }

        [HttpGet("get-last-trip")]
        public async Task<IActionResult> GetLastTrip()
        {
            return Json(_service.GetLastTrip());
        }

        [HttpGet("get-last-entertaiments")]
        public async Task<IActionResult> GetVisitEntertaiment(Guid userId)
        {
            return Json(_service.GetVisitEntertaiment(userId));
        }

        [HttpGet("get-last-user-reviews")]
        public async Task<IActionResult> GetUserLastReview(Guid userId)
        {
            return Json(_service.GetUserLastReview(userId));
        }

        [HttpGet("get-user-last-trip")]
        public async Task<IActionResult> GetUserLastTrip(Guid userId)
        {
            return Json(_service.GetUserLastTrip(userId));
        }
        [HttpGet("get-user-comments")]
        public async Task<IActionResult> GetUserComments(Guid userId)
        {
            return Json(_service.GetUserComments(userId));
        }
    }
}
