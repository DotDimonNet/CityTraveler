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
        public ApplicationUserModel GetUserById(Guid userId);
        public IEnumerable<ApplicationUserModel> GetUsersRange(int skip = 0, int take = 10);
        public IEnumerable<ApplicationUserModel> GetUsers(IEnumerable<Guid> guids);
        public IEnumerable<ApplicationUserModel> GetUsersByPropeties(string name = "", string email = "", string gender= "", DateTime userbirthday =default);
        
    }   
}

