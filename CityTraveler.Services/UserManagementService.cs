using Microsoft.EntityFrameworkCore;
using CityTraveler.Domain.Entities;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CityTraveler.Services
{
    public class UserManagementService// : IUserManagementService
    {
        private ApplicationContext _context;
        public UserManagementService(ApplicationContext context)
        {
            _context = context;
        }
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        
        public async Task<ApplicationUserModel> GetUserById(Guid userId)
        {
                return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }
        public IEnumerable<ApplicationUserModel> GetUsersByBirthday (DateTime userbirthday)
        {
                return _context.Users.Where(x => x.Profile.Birthday.Date == userbirthday.Date);
        }
        public IEnumerable<ApplicationUserModel> GetUsersByName (string name)
        {
            return _context.Users.Where(x => x.Profile.Name == name);
        }
        public IEnumerable<ApplicationUserModel> GetUsersByGender(string gender)
        {
            return _context.Users.Where(x => x.Profile.Gender == gender);
        }
        public IEnumerable<ApplicationUserModel> GetUsersRange(int skip = 0, int take = 10)
        {
            return _context.Users.Skip(skip).Take(take);
        }
        public IEnumerable<ApplicationUserModel> GetUsers(IEnumerable<Guid> guids)
        {
            return _context.Users.Where(x => guids.Contains(x.Id));
        }
        public async Task<ApplicationUserModel> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email); 
        }

        
    }

        
    
}
