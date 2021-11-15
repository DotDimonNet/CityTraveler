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
        private readonly IUserManagementService _service;

        public UserManagementController(IUserManagementService userService)
        {
            _service = userService;
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetUsers([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var users = await _service.GetUsersRangeAsync(skip, take);
            return Json(users);
        }

        [HttpGet("users-search")]
        public async Task<IActionResult> GetUsersByPropeties(
            [FromQuery] string name, 
            [FromQuery] string email, 
            [FromQuery] string gender)
        {
            var users = await _service.GetUsersByPropetiesAsync(name ?? "", email ?? "", gender ?? "");
            return Json(users);
        }

        [HttpPut("user-update")]
        public async Task<IActionResult> UpdateUser([FromQuery] UpdateUserDTO updateUser, string username)
        {
            return Json(await _service.UpdateUser(updateUser, username));
        }

        [HttpDelete("user-delete")]
        public async Task<IActionResult> DeleteUser([FromQuery] string username)
        {
            return Json(await _service.DeleteUser(username));
        }
    }
}
