using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Extensions
{
    public static class DTOExtensions
    {
        public static UserDTO ToUserDTO(this ApplicationUserModel userModel)
        {
            return new UserDTO()
            {
                Name = userModel.Profile.Name,
                AvatarSrc = userModel.Profile.AvatarSrc,
                PhoneNumber = userModel.PhoneNumber,
                Email = userModel.Email
            };
        }

        public static ApplicationUserModel ToApplicationUser(this RegisterDTO registerModel)
        {
            var user = new ApplicationUserModel
            {
                Images = new List<UserImageModel>()
                { new UserImageModel
                {
                    IsMain = true,
                    Source = registerModel.AvatarSrc,
                    Title = "Avatar",
                    Description = "My best photo" } },
                UserName = registerModel.UserName,
                Profile = new UserProfileModel()
                {
                    Gender = registerModel.Gender,
                    Name = registerModel.Name,
                    Birthday = registerModel.Birthday,
                    AvatarSrc = registerModel.AvatarSrc,
                    User = new ApplicationUserModel()
                    {
                        UserName = registerModel.UserName,
                        PasswordHash = registerModel.Password,
                        Email = registerModel.Email,
                        PhoneNumber = registerModel.PhoneNumber

                    }
                }
            };
            return user;
        }
    }
}
