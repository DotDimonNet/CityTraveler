using CityTraveler.Entities.DTO;
using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IUser> Register(RegisterModel model);
        Task<IUser> Login(LoginModel model);
        Task<bool> UpdateRole(UpdateRoleModel model);
        Task<bool> Logout(LogoutModel model);
        Task<bool> Unblock(UnblockModel model);
    }
}
