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
    [Route("api/info")]
    public class InfoController : Controller
    {
        private readonly ILogger<InfoController> _logger;
        private readonly IInfoService _service;

        public InfoController(ILogger<InfoController> logger, IInfoService infoService)
        {
            _service = infoService;
            _logger = logger;
        }
    }
}
