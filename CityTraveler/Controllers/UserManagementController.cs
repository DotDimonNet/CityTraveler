using CityTraveler.Domain.DTO;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using CityTraveler.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserManagementController : Controller
    {
        private readonly ILogger<UserManagementController> _logger;
        private readonly IUserManagementService _service;

        public UserManagementController(ILogger<UserManagementController> logger, IUserManagementService userService)
        {
            _service = userService;
            _logger = logger;
        }

        [HttpGet("id")]
        public IActionResult GetUserById([FromQuery] Guid userId)
        {
            var user =  _service.GetUserById(userId);
            return Json(user);
        }


        [HttpGet("users")]
        public IActionResult GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var users = _service.GetUsersRange(skip, take);
            return Json(users);
        }
             

        [HttpGet("users-by-id")]
        public IActionResult GetUsers ([FromQuery] IEnumerable<Guid> guids)
        {
            var users = _service.GetUsers(guids);
            return Json(users);
        }

        [HttpGet("users-name-email-gender-birthday")]

        public IActionResult GetUsersByPropeties(
            [FromQuery] string name = "", 
            [FromQuery] string email = "", 
            [FromQuery] string gender = "", 
            [FromQuery] DateTime birthday = default)
        {
            var users = _service.GetUsersByPropeties(name, email, gender, birthday);
            return Json(users);
        }

    }
}
