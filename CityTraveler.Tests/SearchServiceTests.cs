using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Errors;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CityTraveler.Tests
{
    public class SearchServiceTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task FilterUsersNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var users = await service.FilterUsers(new FilterUsers { });
            Assert.IsNotNull(users);
            Console.WriteLine(users.Count());
            Console.WriteLine(ArrangeTests.ApplicationContext.Users.Count());
            foreach (ApplicationUserModel user in users)
            {
                Assert.True(user.UserName.Contains(""));
                Assert.True(user.Profile.Gender.Contains(""));
            }
        }
        [Test]
        public async Task FilterUsersAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var users = await service.FilterUsers(new FilterUsers {UserName = "kate", 
                EntertainmentName = "entertainment", Gender = "f"  });
            Assert.IsNotNull(users);
            foreach (ApplicationUserModel user in users)
            {
                Assert.True(user.UserName.Contains("kate"));
                Assert.True(user.Profile.Gender.Contains("f"));
            }
        }
        [Test]
        public void FilterTripsNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var trips = service.FilterTrips(new FilterTrips {});
            Assert.IsNotNull(trips);
            foreach (TripModel trip in trips)
            {
                    Assert.True(trip.Title.Contains(""));
                    Assert.True(trip.Description.Contains(""));
            }
        }
        [Test]
        public void FilterTripsAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var trips = service.FilterTrips(new FilterTrips
            {
                TripStatus = TripStatus.Passed.Id,
                TripEnd = DateTime.Now.AddYears(-2),
                TripStart = DateTime.Now.AddYears(-3),
                Title = "title",
                Description = "description",
                AverageRatingLess = 4,
                AverageRatingMore = 2,
                EntertaimentName = "entertainment",
                PriceLess = 200,
                PriceMore = 100,
                RealSpent = TimeSpan.FromHours(2),
                OptimalSpent = TimeSpan.FromMinutes(50),
                User = "kate",

            }) ;
            Assert.IsNotNull(trips);
            foreach (TripModel trip in trips)
            {
                Assert.True(trip.TripStatus == TripStatus.Passed);
                Assert.True(trip.Title.Contains("title"));
                Assert.True(trip.Description.Contains("description"));
                Assert.True(trip.RealSpent == TimeSpan.FromHours(2));
                Assert.True(trip.OptimalSpent == TimeSpan.FromMinutes(50));
                Assert.True(trip.AverageRating > 2);
                Assert.True(trip.AverageRating < 4);
                Assert.True(trip.AverageRating > 100);
                Assert.True(trip.AverageRating < 200);

            }
        }
        [Test]
        public void FilterTripsThrowsPriceTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext, 
                new ServiceContext(ArrangeTests.ApplicationContext));
            var result = Assert.Throws<SearchServiceException>(() => service.FilterTrips 
            ( new FilterTrips
            {
                PriceLess = 100,
                PriceMore = 200
            })); 
            Assert.That(result.Message, Is.EqualTo("PriceMore cant`b be more than priceLess. " +
                "The same is for rating."));
        }
        [Test]
        public void FilterTripsThrowsRaitingTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var result = Assert.Throws<SearchServiceException>(() => service.FilterTrips(
                new FilterTrips {
                AverageRatingLess = 100,
                AverageRatingMore = 200
            }));
            Assert.That(result.Message, Is.EqualTo("PriceMore cant`b be more than priceLess. " +
                "The same is for rating."));
        }
        [Test]
        public void FilterEntertainmentsNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext, 
                new ServiceContext(ArrangeTests.ApplicationContext));
            var entertainments = service.FilterEntertainments(new FilterEntertainment { });
            Assert.IsNotNull(entertainments);
            foreach (EntertaimentModel entertainment in entertainments) 
            {
                Assert.True(entertainment.Title.Contains(""));
            }

        }
        [Test]
        public void FilterEntertainmentsAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var entertainments = service.FilterEntertainments(new FilterEntertainment
            {
                Title = "title",
                PriceLess = 200,
                PriceMore = 100,
                HouseNumber = "2a",
                StreetName = "a",
                RatingLess = 3,
                RatingMore = 1,
                TripName = "wonder",
                Type = EntertainmentType.Event.Id

            });
            Assert.IsNotNull(entertainments);
            foreach (EntertaimentModel entertainment in entertainments)
            {
                Assert.True(entertainment.Title.Contains("title"));
                Assert.True(entertainment.Type == EntertainmentType.Event);
                Assert.True(entertainment.AverageRating > 1);
                Assert.True(entertainment.AverageRating < 3);
                Assert.True(entertainment.Address.Street.Title.Contains("a"));
                Assert.True(entertainment.Address.HouseNumber.Contains("2a"));
                Assert.True(entertainment.AveragePrice.Value > 100);
                Assert.True(entertainment.AveragePrice.Value < 200);
            }
        }
        [Test]
        public void FilterEnetertainmentThrowsPriceTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var ex = Assert.Throws<SearchServiceException>(() => 
            service.FilterEntertainments(new FilterEntertainment
            {
                PriceLess = 100,
                PriceMore = 200
            }));
            Assert.That(ex.Message, Is.EqualTo("PriceMore cant`b be more than priceLess. The same is for rating."));
        }
        [Test]
        public void FilterEntertainmentThrowsRaitingTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var ex = Assert.Throws<SearchServiceException>(() =>
            service.FilterEntertainments(new FilterEntertainment
            {
                RatingLess = 100,
                RatingMore = 200
            }));
            Assert.That(ex.Message, Is.EqualTo("PriceMore cant`b be more than priceLess. The same is for rating."));
        }
        [Test]
        public void FilterUsersAlikeNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var users = service.FilterUsersAlike(new UserProfileModel {});
            Assert.IsNotNull(users);
            foreach (ApplicationUserModel user in users)
            {
                Assert.True(user.UserName.Contains(""));
                Assert.True(user.Profile.Gender.Contains(""));
            }
        }
        [Test]
        public void FilterUsersAlikeAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var users = service.FilterUsersAlike(new UserProfileModel
            {
                Name = "kate",
                Gender = "f",
                Birthday = DateTime.Now.AddMonths(-2),
                User= new ApplicationUserModel { },
                AvatarSrc = "avatar"
            });
            Assert.IsNotNull(users);
            foreach (ApplicationUserModel user in users)
            {
                Assert.True(user.UserName.Contains("kate"));
                Assert.True(user.Profile.Gender.Contains("f"));
                Assert.True(user.Profile.AvatarSrc.Contains("avatar"));
            }
        }
        [Test]
        public void FilterEnetertainmentsAlikeNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var entertainments = service.FilterEntertainmentsAlike(new EntertaimentModel { });
            Console.WriteLine(ArrangeTests.ApplicationContext.Entertaiments.Count());
            Assert.IsNotNull(entertainments);
        }
        [Test]
        public void FilterEntertainmentsAlikeAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var entertainments = service.FilterEntertainmentsAlike(new EntertaimentModel
            {
                Description = "desc",
                AverageRating = 5,
                Title = "title",
                Type = EntertainmentType.Institution

            });
            Assert.IsNotNull(entertainments);
            foreach (EntertaimentModel entertainment in entertainments)
            {
                Assert.True(entertainment.Description.Contains("desc"));
                Assert.True(entertainment.AverageRating == 5);
                Assert.True(entertainment.Title.Contains("title"));
                Assert.True(entertainment.Type == EntertainmentType.Institution);
            }
        }
        [Test]
        public void FilterTripsAlikeNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var trips = service.FilterTripsAlike(new TripModel { });
            Assert.IsNotNull(trips);
            foreach (TripModel trip in trips)
            {
                Assert.True(trip.Description.Contains(""));
                Assert.True(trip.Title.Contains(""));
            }
        }
        [Test]
        public void FilterTripsAlikeAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var trips = service.FilterTripsAlike(new TripModel
            {
                Description = "desc",
                AverageRating = 5,
                Title = "title",
                Price = new TripPriceModel { }
            });
            Assert.IsNotNull(trips);
            foreach (TripModel trip in trips)
            {
                Assert.True(trip.Description.Contains("desc"));
                Assert.True(trip.AverageRating == 5);
                Assert.True(trip.Title.Contains("title"));
            }
        }

    }
}
