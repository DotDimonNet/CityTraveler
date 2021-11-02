using Microsoft.EntityFrameworkCore;
using CityTraveler.Domain.Errors;
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
    public class UserManagementService : IUserManagementService
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


        public ApplicationUserModel GetUserById(Guid userId)
        {
            if (_context.Users.FirstOrDefault(x => x.Id == userId) == null)
            {
                throw new UserManagemenServicetException("Users not found");
            }
                
            if (userId == Guid.Empty)
            {
                throw new UserManagemenServicetException("Invalid argument");
            }

            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }
                
        public IEnumerable<ApplicationUserModel> GetUsersRange(int skip = 0, int take = 10)
        {
            if (skip < 0 || take < 0)
            {
                throw new UserManagemenServicetException("Invalid arguments");
            }
                
            return _context.Users.Skip(skip).Take(take);
        }
        public IEnumerable<ApplicationUserModel> GetUsers(IEnumerable<Guid> guids)
        {
            return _context.Users.Where(x => guids.Contains(x.Id));
        }
        public IEnumerable<ApplicationUserModel> GetUsersByPropeties(string name = "", string email = "", string gender = "", DateTime birthday = default)
        {
            if (_context.Users
                .Where(x => x.Profile.Name.Contains(name) && x.Profile.Gender.Contains(gender) && x.Email
                .Contains(email) && x.Profile.Birthday.Date == birthday.Date) == null)
            {
                throw new UserManagemenServicetException("Users not found");
            }
            return _context.Users
                .Where(x => x.Profile.Name.Contains(name) && x.Profile.Gender.Contains(gender) && x.Email.Contains(email) && x.Profile.Birthday.Date == birthday.Date);
        }
    }

        
    
}
