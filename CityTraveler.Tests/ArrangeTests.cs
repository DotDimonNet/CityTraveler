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
            //StreetGen
            var streets = new List<StreetModel>();
            for (int i = 0; i < 10; i++)
            {
                var street = new StreetModel()
                {
                    Title = $"Street-{i}",
                    Description = $"Street description-{i}"
                };
                streets.Add(street);
            }
            //EntertainmentGen
            var entertainments = new List<EntertaimentModel>();
            for (int i = 0; i < 100; i++)
            {
                var rnd = new Random();
                var streetIndex = rnd.Next(0, 9);

                var entertainmentType = EntertainmentType.Landskape;
                switch (i % 3)
                {
                    case 0:
                        entertainmentType = EntertainmentType.Event;
                        break;
                    case 1:
                        entertainmentType = EntertainmentType.Institution;
                        break;
                }
                var entertainment = new EntertaimentModel()
                {
                    Address = new AddressModel()
                    {
                        Coordinates = new CoordinatesModel()
                        {
                            Latitude = i * 3 / 2 + 1.34,
                            Longitude = i * 5 / 2 + 1.34
                        },
                        HouseNumber = $"House-{i}",
                        ApartmentNumber = $"Apartment-{i}",
                        Street = streets[streetIndex],
                    },
                    AveragePrice = new EntertaimentPriceModel(),
                    Reviews = new List<EntertainmentReviewModel>()
                    {
                        new EntertainmentReviewModel() { Rating = new RatingModel() { Value = rnd.Next(1, 5) } },
                        new EntertainmentReviewModel() { Rating = new RatingModel() { Value = rnd.Next(1, 5) } },
                        new EntertainmentReviewModel() { Rating = new RatingModel() { Value = rnd.Next(1, 5) } },
                        new EntertainmentReviewModel() { Rating = new RatingModel() { Value = rnd.Next(1, 5) } },
                    },
                    Type = entertainmentType,
                };
                entertainments.Add(entertainment);
            }
            await ApplicationContext.Streets.AddRangeAsync(streets);
            await ApplicationContext.SaveChangesAsync();
            await ApplicationContext.Entertaiments.AddRangeAsync(entertainments);
            await ApplicationContext.SaveChangesAsync();
        }
    }
}
