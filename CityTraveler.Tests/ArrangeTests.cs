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
            await GenerateReviews();
            await GenerateComment();
            await GenerateImage();
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
            await ApplicationContext.Entertaiments.AddRangeAsync(entertainments);
            await ApplicationContext.SaveChangesAsync();
        }
        private static async Task GenerateReviews()
        {
            var reviews = new List<ReviewModel>();
            for (int i = 0; i < 10; i++)
            {
                var review = new TripReviewModel()
                {
                    User = new ApplicationUserModel { Profile = new UserProfileModel { Name = "lll" } },
                    Trip = new TripModel { },
                    Rating = new RatingModel { Value = 5}
                };

                reviews.Add(review);
            }
            await ApplicationContext.Reviews.AddRangeAsync(reviews);
            await ApplicationContext.SaveChangesAsync();
        }
        private static async Task GenerateComment()
        {
            var comments = new List<CommentModel>();
            for (int i = 0; i < 10; i++)
            {
                var comment = new CommentModel()
                {
                    Status = CommentStatus.Liked,
                    Review = new ReviewModel { Rating = new RatingModel { Value = 3 } }
                };

                comments.Add(comment);
            }
            await ApplicationContext.Comments.AddRangeAsync(comments);
            await ApplicationContext.SaveChangesAsync();
        }
        private static async Task GenerateImage()
        {
            var images = new List<ImageModel>();
            for (int i = 0; i < 10; i++)
            {
                var image = new ReviewImageModel()
                {
                    Review = new ReviewModel {
                        User = new ApplicationUserModel { Profile = new UserProfileModel { Name = "lll" } },
                        Rating = new RatingModel { Value = 5 }
                    }

                };

                images.Add(image);
            }
            await ApplicationContext.Images.AddRangeAsync(images);
            await ApplicationContext.SaveChangesAsync();
        }
    }
}
