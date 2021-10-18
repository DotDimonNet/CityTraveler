using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    /*public class AuthService : IAuthService
    {
        private readonly DbContext _context;
        public AuthService(DbContext context)
        {
            _context = context;
        }

        public async Task<IUser> Login(LoginModel model)
        {
            var query = $"";

            return await _context.Users.RequestManager.ExecuteScalarAsync(query, null, false);
        }

        public async Task<IUser> Register(RegisterModel model)
        {
            var query = $"";

            return await _context.Users.RequestManager.ExecuteScalarAsync(query, null, false);
        }

        public async Task<bool> Logout(LogoutModel model)
        {
            var query = $"";
            var affectedUsers = await _context.Users.RequestManager.SendRequestAsync(query, null, false);

            return affectedUsers > 0;
        }


        public async Task<bool> Unblock(UnblockModel model)
        {
            var query = $"";
            var affectedUsers = await _context.Users.RequestManager.SendRequestAsync(query, null, false);

            return affectedUsers > 0;
        }

        public async Task<bool> UpdateRole(UpdateRoleModel model)
        {
            var query = $"";
            var affectedUsers = await _context.Users.RequestManager.SendRequestAsync(query, null, false);

            return affectedUsers > 0;
        }
    }*/
}
