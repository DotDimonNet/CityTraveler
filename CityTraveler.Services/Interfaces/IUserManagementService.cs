using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IUserManagementService : IServiceMetadata
    {
        public UserDTO GetUserById(Guid userId);
        public IEnumerable<UserDTO> GetUsersRange(int skip = 0, int take = 10);
        public IEnumerable<UserDTO> GetUsers(IEnumerable<Guid> guids);
        public IEnumerable<UserDTO> GetUsersByPropeties(string name = "", string email = "", string gender= "", DateTime userbirthday =default);
        
    }   
}

