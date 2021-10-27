using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Infrastructure.Authorization;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Extensions;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;
        private readonly SignInManager<ApplicationUserModel> _signInManager;
        public AuthService(ApplicationContext context, SignInManager<ApplicationUserModel> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
     
        public async Task<UserDTO> Register(RegisterDTO registerModel)
        {
            
            var user = registerModel.ToApplicationUser();
            
            var result = await _signInManager.UserManager.CreateAsync(user, registerModel.Password);
                                   
            if (result.Succeeded)
            {
                
                user = await _signInManager.UserManager.FindByNameAsync(registerModel.UserName);
                var rolesResult = await _signInManager.UserManager.AddToRoleAsync(user, Roles.User);
                 if (rolesResult.Succeeded)
                {
                    
                    var signInResult = await _signInManager.PasswordSignInAsync(user, registerModel.Password, true, false);
                    if (signInResult.Succeeded)
                    {

                        return user.ToUserDTO(); 
                    }
                    else
                    {
                        throw new Exception("User was registered, but cannot signed in. Please confirm your profile.");

                    }
                }
                 else
                {
                    await _signInManager.UserManager.DeleteAsync(user);
                    throw new Exception("User was not registered.");
                }
                
            }
            else
            {
                throw new Exception("User was not registered.");
            }
        }
      
        
        public async Task<UserDTO> UpdateUserData(UpdateUserDTO updateUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == updateUser.UserName);
            user.Profile.Name = updateUser.Name;
            user.Profile.AvatarSrc = updateUser.AvatarSrc;
            user.Profile.User.Email = updateUser.Email;
            user.Profile.User.PhoneNumber = updateUser.PhoneNumber;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user.ToUserDTO();

        }

        public async Task<UserDTO> UpdateUserPassword(UpdateUserPasswordDTO updateUserPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == updateUserPassword.UserName);
            var result = await _signInManager.UserManager.ChangePasswordAsync(user, updateUserPassword.OldPassword, updateUserPassword.NewPassword);
            if (result.Succeeded)
            {
                return user.ToUserDTO();
            }
            else
            {
                throw new Exception("");
            }
        }
        public async Task<bool> SighOutUser ()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public async Task<bool> DeleteUserProfile(Guid userId)
        {
            try
            {
                var user = await _signInManager.UserManager.FindByIdAsync(userId.ToString());

                if (user != null)
                {
                    var result = await _signInManager.UserManager.DeleteAsync(user);

                    return result.Succeeded;
                }
                else
                {
                    throw new Exception("User was not found!");
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
    }
}
