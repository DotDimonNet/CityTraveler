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
    [Route("api/userManagement")]
    public class UserManagementController : Controller
    {
        private readonly ILogger<UserManagementController> _logger;
        private readonly IUserManagementService _service;

        public UserManagementController(ILogger<UserManagementController> logger, IUserManagementService userService)
        {
            _service = userService;
            _logger = logger;
        }

        [HttpGet]
        [Route("get/user/id/{userId}")]

        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Json(_service.GetUserById(userId));
        }

        [HttpGet]
        [Route("get/user/birthday/{userbirthday}")]

        public async Task<IActionResult> GetUsersByBirthday(DateTime userbirthday)
        {
            return Json(_service.GetUsersByBirthday(userbirthday));
        }

        [HttpGet]
        [Route("get/user/name/{name}")]

        public async Task<IActionResult> GetUsersByName(string name)
        {
            return Json(_service.GetUsersByName(name));
        }

        [HttpGet]
        [Route("get/user/gender/{gender}")]

        public async Task<IActionResult> GetUsersByGender(string gender)
        {
            return Json(_service.GetUsersByGender(gender));
        }

        [HttpGet]
        [Route("get/user")]

        public async Task<IActionResult> GetUsers(int skip = 0, int take = 10)
        {
            return Json(_service.GetUsersRange(skip, take));
        }

        [HttpGet]
        [Route("get/user/email/{email}")]

        public async Task<IActionResult> GetUserByEmail(string email)
        {
            return Json(_service.GetUserByEmail (email));
        }

        [HttpGet]
        [Route("get/user/id/{guids}")]

        public async Task<IActionResult> GetUsers (IEnumerable<Guid> guids)
        {
            return Json(_service.GetUsers(guids));
        }



    }
}
