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
        public IActionResult GetUserById(Guid userId)
        {
            return Json(_service.GetUserById(userId));
        }

        [HttpGet("birthday/{date}")]
        public IActionResult GetUsersByBirthday(DateTime date)
        {
            return Json(_service.GetUsersByBirthday(date));
        }

        [HttpGet("name/{name}")]
        public IActionResult GetUsersByName(string name)
        {
            return Json(_service.GetUsersByName(name));
        }

        [HttpGet("gender/{gender}")]
        public IActionResult GetUsersByGender(string gender)
        {
            return Json(_service.GetUsersByGender(gender));
        }

        [HttpGet("users")]
        public IActionResult GetUsers(int skip = 0, int take = 10)
        {
            return Json(_service.GetUsersRange(skip, take));
        }

        [HttpGet("email/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            return Json(_service.GetUserByEmail (email));
        }

        [HttpGet("users-by-id")]
        public IActionResult GetUsers (IEnumerable<Guid> guids)
        {
            return Json(_service.GetUsers(guids));
        }

    }
}
