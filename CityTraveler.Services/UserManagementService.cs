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
                throw new UserManagemenServicetException("Users not found");
            if (userId == Guid.Empty)
                throw new UserManagemenServicetException("Invalid argument");

            return _context.Users.FirstOrDefault(x => x.Id == userId);
        }
        public IEnumerable<ApplicationUserModel> GetUsersByBirthday(DateTime userbirthday)
        {
            if (_context.Users.Where(x => x.Profile.Birthday.Date == userbirthday).Count() == 0)
                throw new UserManagemenServicetException("Users not found");
            if (userbirthday.Date > DateTime.Now)
                throw new UserManagemenServicetException("Invalid argument");

            return _context.Users.Where(x => x.Profile.Birthday.Date == userbirthday.Date);
        }
        public IEnumerable<ApplicationUserModel> GetUsersByName(string name)
        {
            if (_context.Users.Where(x => x.Profile.Name == name).Count() == 0)
                throw new UserManagemenServicetException("Users not found");
            if (name == null)
                throw new UserManagemenServicetException("Invalid argument");
            return _context.Users.Where(x => x.Profile.Name == name);
        }
        public IEnumerable<ApplicationUserModel> GetUsersByGender(string gender)
        {
            if (gender == null)
                throw new UserManagemenServicetException("Invalid argument");

            return _context.Users.Where(x => x.Profile.Gender == gender);
        }
        public IEnumerable<ApplicationUserModel> GetUsersRange(int skip = 0, int take = 10)
        {
            if (skip < 0 || take < 0)
                throw new UserManagemenServicetException("Invalid arguments");
            return _context.Users.Skip(skip).Take(take);
        }
        public IEnumerable<ApplicationUserModel> GetUsers(IEnumerable<Guid> guids)
        {
            return _context.Users.Where(x => guids.Contains(x.Id));
        }
        public ApplicationUserModel GetUserByEmail(string email)
        {
            if (email == null)
                throw new UserManagemenServicetException("Invalid argument");
            if (_context.Users.FirstOrDefault(x => x.Email == email) == null)
                throw new UserManagemenServicetException("User not found");
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

    }

        
    
}
