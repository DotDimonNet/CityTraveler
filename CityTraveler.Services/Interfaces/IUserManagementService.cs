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
        public bool UpdateUserData(UserProfileModel user);
        public bool RemoveUser(Guid userId);
        public IEnumerable<UserProfileModel> GetUsers(int skip = 0, int take = 10);
        public IEnumerable<UserProfileModel> GetUsers(IEnumerable<Guid> guids);
        public UserProfileModel GetUserById(Guid userId);
        //public UserProfileModel GetUserByEmail(string email);

        
    }   
}

