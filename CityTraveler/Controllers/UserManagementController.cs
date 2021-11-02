using CityTraveler.Repository.DbContext;
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

        [HttpGet("id/{userId}")]
        public IActionResult GetUserById([FromQuery] Guid userId)
        {
            return Json(_service.GetUserById(userId));
        }


        [HttpGet("users")]
        public IActionResult GetUsers([FromQuery] int skip = 0, int take = 10)
        {
            return Json(_service.GetUsersRange(skip, take));
        }
             

        [HttpGet("users-by-id")]
        public IActionResult GetUsers ([FromQuery] IEnumerable<Guid> guids)
        {
            return Json(_service.GetUsers(guids));
        }

        [HttpGet("users-name-email-gender-birthday")]

        public IActionResult GetUsersByPropeties([FromQuery] string name = "", string email = "", string gender = "", DateTime birthday = default)
        {
            return Json(_service.GetUsersByPropeties(name, email, gender, birthday));
        }

    }
}
