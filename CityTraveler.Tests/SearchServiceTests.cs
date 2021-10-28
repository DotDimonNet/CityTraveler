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
        public void FilterUsersNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var users = service.FilterUsers(new FilterUsers { });
            Assert.IsNotNull(users);
        }
        [Test]
        public void FilterUsersAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var users = service.FilterUsers(new FilterUsers {UserName = "kate", 
                EntertainmentName = "entertainment", Gender = "f"  });
            Assert.IsNotNull(users);
        }
        [Test]
        public void FilterTripsNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var trips = service.FilterTrips(new FilterTrips {});
            Assert.IsNotNull(trips);
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
        }
        [Test]
        public void FilterEntertainmentsAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var users = service.FilterEntertainments(new FilterEntertainment
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
            Assert.IsNotNull(users);
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
        }
        [Test]
        public void FilterEnetertainmentsAlikeNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var entertainments = service.FilterEntertainmentsAlike(new EntertaimentModel { });
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
                Address = new AddressModel { },
                AveragePrice = new EntertaimentPriceModel { },
                AverageRating = 5,
                Title = "title",
                Type = EntertainmentType.Institution

            });
            Assert.IsNotNull(entertainments);
        }
        [Test]
        public void FilterTripsAlikeNullFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var entertainments = service.FilterTripsAlike(new TripModel { });
            Assert.IsNotNull(entertainments);
        }
        [Test]
        public void FilterTripsAlikeAllFieldsTest()
        {
            var service = new SearchService(ArrangeTests.ApplicationContext,
                new ServiceContext(ArrangeTests.ApplicationContext));
            var entertainments = service.FilterTripsAlike(new TripModel
            {
                Description = "desc",
                AverageRating = 5,
                Title = "title",
                Price = new TripPriceModel { }
            });
            Assert.IsNotNull(entertainments);
        }

    }
}
