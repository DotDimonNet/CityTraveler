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
        public Task<UserProfileModel> GetUserById(Guid userId);
        public IEnumerable<UserProfileModel> GetUsersByBirthday(DateTime userbirthday);
        public IEnumerable<UserProfileModel> GetUsersByName(string name);
        public IEnumerable<UserProfileModel> GetUsersByGender(string gender);
        public IEnumerable<UserProfileModel> GetUsers(int skip = 0, int take = 10);
        public IEnumerable<UserProfileModel> GetUsers(IEnumerable<Guid> guids);
        public Task<UserProfileModel> GetUserByEmail(string email);
    }   
}

