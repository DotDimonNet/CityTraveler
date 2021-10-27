using CityTraveler.Domain.Entities;
using CityTraveler.Infrastucture.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Tests
{
    public static class ArrangeTests
    {
        public static ApplicationContext ApplicationContext { get; set; }

        public static Mock<UserManager<ApplicationUserModel>> UserManagerMock;
        public static Mock<SignInManager<ApplicationUserModel>> SignInManagerMock;
        public static Mock<RoleManager<ApplicationUserRole>> RoleManagerMock;

        public static async Task SetupDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            ApplicationContext = new ApplicationContext(options);
            SetupManagementMocks();
            var dbInitializer = new DbInitializer(ApplicationContext, UserManagerMock.Object, RoleManagerMock.Object);

            await dbInitializer.Initialize();
            await GenerateData();
        }

        private static void SetupManagementMocks()
        {
            var user = new ApplicationUserModel
            {
                UserName = "admin@admin.admin",
                Email = "admin@admin.admin",
                EmailConfirmed = true,
                Profile = new UserProfileModel
                {
                    Gender = "male",
                }
            };
            var store = new Mock<IUserStore<ApplicationUserModel>>();
            UserManagerMock = new Mock<UserManager<ApplicationUserModel>>(store.Object, null, null, null, null, null, null, null, null);
            UserManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>())).Callback(() => 
            {
                ApplicationContext.Users.Add(user);
                ApplicationContext.SaveChanges();
            }).ReturnsAsync(IdentityResult.Success).Verifiable();
            UserManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();
            UserManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            
            var storeRoles = new Mock<IRoleStore<ApplicationUserRole>>();
            RoleManagerMock = new Mock<RoleManager<ApplicationUserRole>>(storeRoles.Object, null, null, null, null);
            RoleManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUserRole>())).ReturnsAsync(IdentityResult.Success).Verifiable();
        }

        private static async Task GenerateData()
        {
            var entertainments = new List<EntertaimentModel>();
            for (int i = 0; i < 10; i++)
            {
                var entertainment = new EntertaimentModel()
                {
                    Address = new AddressModel(),
                    AveragePrice = new EntertaimentPriceModel(),
                };

                entertainments.Add(entertainment);
            }
            await ApplicationContext.Entertaiments.AddRangeAsync(entertainments);
            await ApplicationContext.SaveChangesAsync();

            var userProfiles = new List<UserProfileModel>();
            for (int i = 0; i < 10; i++)
            {
                var user = new UserProfileModel()
                {
                    Name = $"name{i}",
                    Birthday = new DateTime(),
                    Gender = "male",
                    User = new ApplicationUserModel()
                    {
                        UserId = new Guid(),
                        UserName = $"email{i}@email",
                        Email = $"email{i}@email",
                    }
                };

                userProfiles.Add(user);
            }
            await ApplicationContext.UserProfiles.AddRangeAsync(userProfiles);
            await ApplicationContext.SaveChangesAsync();
        }
    }
}
