using CityTraveler.Domain.Entities;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CityTraveler.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly ApplicationContext _context;
        private readonly ServiceContext _svContext;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public AdminPanelService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ApplicationUserModel>> AdminFilterUsers(FilterAdminUser filter)
        {
            
            try
            {
                var users =  _context.Users.Where(x =>
                    x.UserName.Contains(filter.UserName ?? "")
                    && x.Profile.Gender.Contains(filter.Gender ?? "")
                    && x.Profile.Name.Contains(filter.Name ?? "")
                    && x.Email.Contains(filter.Email ?? "")
                    && x.PhoneNumber.Contains(filter.PhoneNumber ?? ""));
                    
                    
                return users;
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn`t filter users, {e.Message}");
            }
        }
    }
}
