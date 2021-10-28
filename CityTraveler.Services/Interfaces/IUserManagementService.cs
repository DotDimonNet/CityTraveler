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
        public Task<ApplicationUserModel> GetUserById(Guid userId);
        public IEnumerable<ApplicationUserModel> GetUsersByBirthday(DateTime userbirthday);
        public IEnumerable<ApplicationUserModel> GetUsersByName(string name);
        public IEnumerable<ApplicationUserModel> GetUsersByGender(string gender);
        public IEnumerable<ApplicationUserModel> GetUsersRange(int skip = 0, int take = 10);
        public IEnumerable<ApplicationUserModel> GetUsers(IEnumerable<Guid> guids);
        public Task<ApplicationUserModel> GetUserByEmail(string email);
    }   
}

