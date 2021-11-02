
using CityTraveler.Domain.DTO;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("entertainment")]
        public async Task<IActionResult> AddEntertainment([FromBody] EntertainmentDTO dtoModel)
        {
            return Json(await _service.AddEntertainment(dtoModel));
        }

        [HttpDelete]
        [Route("remove-entertainment")]
        public async Task<IActionResult> RemoveEntertainment(string title) 
        {
            var entertainmentId = _entertainmentService.GetEntertainmentByTitle(title).Select(x=>x.Id).FirstOrDefault();
            return Json(await _service.RemoveEntertainment(entertainmentId));
        }
    }
}
