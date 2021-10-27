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

        
        public async Task<UserProfileModel> GetUserById(Guid userId)
        {
           
                return await _context.UserProfiles.FirstOrDefaultAsync(x => x.Id == userId);
                              
                       
        }

        public IEnumerable<UserProfileModel> GetUsersByBirthday (DateTime userbirthday)
        {
            
                return _context.UserProfiles.Where(x => x.Birthday.Date == userbirthday.Date);
                     
           
        }

        public IEnumerable<UserProfileModel> GetUsersByName (string name)
        {
            return _context.UserProfiles.Where(x => x.Name == name);
        }

        public IEnumerable<UserProfileModel> GetUsersByGender(string gender)
        {
            return _context.UserProfiles.Where(x => x.Gender == gender);
        }

        public IEnumerable<UserProfileModel> GetUsers(int skip = 0, int take = 10)
        {
            return _context.UserProfiles.Skip(skip).Take(take);
        }

        public IEnumerable<UserProfileModel> GetUsers(IEnumerable<Guid> guids)
        {
            return _context.UserProfiles.Where(x => guids.Contains(x.Id));
        }

        public async Task<bool> RemoveUser(Guid userId)
        {
            try
            {
                _context.UserProfiles.Where(x => x.Id != userId);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        
        }

        
    }

        
}
