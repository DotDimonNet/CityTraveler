using Microsoft.EntityFrameworkCore;
using CityTraveler.Domain.Errors;
using CityTraveler.Domain.Entities;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services
{
    public class UserManagementService : IUserManagementService
    {
        private ApplicationContext _context;
        private readonly ILogger<UserManagementService> _logger;
        private readonly IMapper _mapper;

        public UserManagementService(ApplicationContext context, IMapper mapper, ILogger<UserManagementService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public UserManagementService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public UserDTO GetUserById(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (user != null)
            {
                return _mapper.Map<ApplicationUserModel, UserDTO>(user);
            }
            else
            {
                throw new UserManagemenServicetException("Users not found");
            }

        }
                
        public IEnumerable<UserDTO> GetUsersRange(int skip = 0, int take = 10)
        {
            try
            {
                if (skip < 0 || take < 0)
                {
                    throw new UserManagemenServicetException("Invalid arguments");
                }

                var users = _context.Users.Skip(skip).Take(take);

            return _mapper.Map<IEnumerable<ApplicationUserModel>, IEnumerable<UserDTO>>(users);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new UserManagemenServicetException($"Users not found: {e.Message}");
            }
           
        }
        public IEnumerable<UserDTO> GetUsers(IEnumerable<Guid> guids)
        {
            var users = _context.Users.Where(x => guids.Contains(x.Id));
            if (users != null)
            {
                return _mapper.Map<IEnumerable<ApplicationUserModel>, IEnumerable<UserDTO>>(users);
            }
            else
            {
                throw new UserManagemenServicetException("Users not found");
            }
            
        }
        public IEnumerable<UserDTO> GetUsersByPropeties(string name = "", string email = "", string gender = "", DateTime birthday = default)
        {
            try
            {
                var users = _context.Users
                .Where(x => x.Profile.Name.Contains(name) && x.Profile.Gender
                .Contains(gender) && x.Email.Contains(email) && x.Profile.Birthday.Date == birthday.Date);

                if (users != null)
                {
                    return _mapper.Map<IEnumerable<ApplicationUserModel>, IEnumerable<UserDTO>>(users);
                }
                else
                { 
                    throw new UserManagemenServicetException("Users not found");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new UserManagemenServicetException($"Users not found: {e.Message}");
            }

        }
    }

        
    
}
