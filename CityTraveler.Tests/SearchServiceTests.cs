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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace CityTraveler.Tests
{
    public class SearchServiceTests
    {
        private Mock<ILogger<SearchService>> _loggerMock;
        private SearchService _service;

        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<SearchService>());
            _service = new SearchService(ArrangeTests.ApplicationContext, _loggerMock.Object,
                new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.SetupTestLogger(new NullLogger<EntertainmentService>()).Object));
        }

        [Test]
        public async Task FilterUsersNullFieldsTest()
        {
            var users = await _service.FilterUsers(new FilterUsers { });
            Assert.IsNotNull (users);

            foreach (ApplicationUserModel user in users.ToList())
            {
                Assert.NotNull(user.UserName);
                Assert.NotNull(user.Profile.Gender);
            }
        }

        [Test]
        public async Task FilterUsersAllFieldsTest()
        {
            var users = await _service.FilterUsers(new FilterUsers
            {
                UserName = "kate",
                EntertainmentName = "entertainment",
                Gender = "f"
            });
            Assert.IsNotNull(users);
            foreach (ApplicationUserModel user in users)
            {
                Assert.True(user.UserName.Contains("kate"));
                Assert.True(user.Profile.Gender.Contains("f"));
            }
        }

        [Test]
        public async Task FilterTripsNullFieldsTest()
        {
            var trips = await _service.FilterTrips(new FilterTrips { });
            Assert.IsNotNull(trips);
            foreach (TripModel trip in trips)
            {
                Assert.AreNotEqual(trip.Title, null);
                Assert.AreNotEqual(trip.Description, null);
            }
        }

        [Test]
        public async Task FilterTripsAllFieldsTest()
        {
            var trips = await _service.FilterTrips(new FilterTrips
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

            });
            Assert.IsNotNull(trips);

            foreach (TripModel trip in trips)
            {
                Assert.AreEqual(trip.TripStatus, TripStatus.Passed);
                Assert.True(trip.Title.Contains("title"));
                Assert.True(trip.Description.Contains("description"));
                Assert.AreEqual(trip.RealSpent, TimeSpan.FromHours(2));
                Assert.AreEqual(trip.OptimalSpent, TimeSpan.FromMinutes(50));
                Assert.True(trip.AverageRating > 2);
                Assert.True(trip.AverageRating < 4);
                Assert.True(trip.AverageRating > 100);
                Assert.True(trip.AverageRating < 200);
            }
        }

        [Test]
        public async Task FilterTripsThrowsPriceTest()
        {
            var result = await _service.FilterTrips(
                new FilterTrips
                {
                    PriceLess = 100,
                    PriceMore = 200
                });
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task FilterTripsThrowsRaitingTest()
        {
            var result = await _service.FilterTrips(
                new FilterTrips
                {
                    AverageRatingLess = 100,
                    AverageRatingMore = 200
                });
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task FilterEntertainmentsNullFieldsTest()
        {
            var entertainments = await _service.FilterEntertainments(new FilterEntertainment { });
            Assert.IsNotNull(entertainments);
            foreach (EntertaimentModel entertainment in entertainments)
            {
                Assert.AreNotEqual(entertainment.Title, null);
            }

        }

        [Test]
        public async Task FilterEntertainmentsAllFieldsTest()
        {
            var entertainments = await _service.FilterEntertainments(new FilterEntertainment
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
                Assert.AreEqual(entertainment.Type, EntertainmentType.Event);
                Assert.True(entertainment.AverageRating > 1);
                Assert.True(entertainment.AverageRating < 3);
                Assert.True(entertainment.Address.Street.Title.Contains("a"));
                Assert.True(entertainment.Address.HouseNumber.Contains("2a"));
                Assert.True(entertainment.AveragePrice.Value > 100);
                Assert.True(entertainment.AveragePrice.Value < 200);
            }
        }

        [Test]
        public async Task FilterEnetertainmentThrowsPriceTest()
        {
            var result = await _service.FilterEntertainments(new FilterEntertainment
            {
                PriceLess = 100,
                PriceMore = 200
            });
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task FilterEntertainmentThrowsRaitingTest()
        {
            var result = await _service.FilterEntertainments(new FilterEntertainment
            {
                RatingLess = 100,
                RatingMore = 200
            });
            Assert.IsEmpty(result);
        }

    }
}
