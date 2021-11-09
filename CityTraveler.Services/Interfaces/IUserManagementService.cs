using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IUserManagementService 
    {
        public Task<ApplicationUserModel> GetUserByIdAsync(Guid userId);
        public Task<IEnumerable<UserDTO>> GetUsersRangeAsync(int skip = 0, int take = 10);
        public Task<IEnumerable<UserDTO>> GetUsersAsync(IEnumerable<Guid> guids);
        public Task<IEnumerable<UserDTO>> GetUsersByPropetiesAsync(string name = "", string email = "", string gender= "", DateTime userbirthday =default);
        
    }   
}

