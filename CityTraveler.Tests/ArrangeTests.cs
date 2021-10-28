using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
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
            await GenerateUserData();

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
                    Gender = "male"
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
            var users = new List<UserProfileModel>();
            for (int i = 0; i < 10; i++)
            {
                var user = new UserProfileModel()
                {
                    Birthday = new DateTime(2018 - i, 9, 13),
                    Id = Guid.NewGuid(),
                    User = new ApplicationUserModel
                    {
                        Trips = new List<TripModel>
                        {
                           new TripModel {AverageRating = i ,TripStatus= TripStatus.Passed},
                           new TripModel {AverageRating = i+ 1,TripStatus= TripStatus.Passed},
                           new TripModel {AverageRating = i+ 4,TripStatus= TripStatus.InProgress}
                        }
                    }
                };

                users.Add(user);
            }
            await ApplicationContext.UserProfiles.AddRangeAsync(users);
            await ApplicationContext.Entertaiments.AddRangeAsync(entertainments);
            await ApplicationContext.SaveChangesAsync();
        }

        private static async Task GenerateUserData()
        {
            var users = new List<UserProfileModel>();
            for (int i = 0; i < 10; i++)
            {
                var user = new UserProfileModel()
                {
                    Birthday = new DateTime(2018 - i, 9, 13),
                    Id = Guid.NewGuid(),
                    User = new ApplicationUserModel
                    {
                        Trips = new List<TripModel>
                        {
                           new TripModel {AverageRating = i ,TripStatus= TripStatus.Passed},
                           new TripModel {AverageRating = i+ 1,TripStatus= TripStatus.Passed},
                           new TripModel {AverageRating = i+ 4,TripStatus= TripStatus.InProgress}
                        }
                    }
                };

                users.Add(user);
            }
            await ApplicationContext.UserProfiles.AddRangeAsync(users);
            await ApplicationContext.SaveChangesAsync();
        }
    }
}
