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

        const string messageExceptionObjectNull = "User not found";
        const string messageExceptionArgument = "Invalid arguments";




        public async Task<ApplicationUserModel> GetUserByIdAsync(Guid userId)
        {
            try
            { 
                var userExist = await _context.Users.AnyAsync(x => x.Id == userId);

                if (userExist)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                    return user;
                }
                throw new UserManagemenServiceException(messageExceptionObjectNull);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                 return null;
            }
        }
                
        public async Task<IEnumerable<UserDTO>> GetUsersRangeAsync(int skip = 0, int take = 10)
        {
            try
            {
                if (skip < 0 || take < 0)
                {
                    throw new UserManagemenServiceException(messageExceptionArgument);
                }

                var users = await Task.Run(() => _context.Users.Skip(skip).Take(take));
                return _mapper.Map<IEnumerable<ApplicationUserModel>, IEnumerable<UserDTO>>(users);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<UserDTO>();
            }
           
        }
        public async Task<IEnumerable<UserDTO>> GetUsersAsync(IEnumerable<Guid> guids)
        {
            try
            {
                var users = await Task.Run(() => _context.Users.Where(x => guids.Contains(x.Id)));
                return _mapper.Map<IEnumerable<ApplicationUserModel>, IEnumerable<UserDTO>>(users);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<UserDTO>();
            }

        }

        public async Task<IEnumerable<UserDTO>> GetUsersByPropetiesAsync(string name = "", string email = "", string gender = "", DateTime birthday = default)
        {
            try
            {
                var users = await Task.Run(() =>  _context.Users
                .Where(x => x.Profile.Name.Contains(name) && x.Profile.Gender
                .Contains(gender) && x.Email.Contains(email) && x.Profile.Birthday.Date == birthday.Date));

                return _mapper.Map<IEnumerable<ApplicationUserModel>, IEnumerable<UserDTO>>(users);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<UserDTO>();
            }
        }
    }

        
    
}
