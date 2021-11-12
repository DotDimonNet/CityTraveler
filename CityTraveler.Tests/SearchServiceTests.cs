using System;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Services;
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
            _service = new SearchService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, _loggerMock.Object);
        }

        [Test]
        public async Task FilterUsersNullFieldsTest()
        {
            var users = await _service.FilterUsers(new FilterUsers { });
            Assert.IsNotNull (users);

            foreach (var user in users.ToList())
            {
                Assert.NotNull(user.Name);
                Assert.NotNull(user.Gender);
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
            foreach (var user in users)
            {
                Assert.True(user.Name.Contains("kate"));
                Assert.True(user.Gender.Contains("f"));
            }
        }

        [Test]
        public async Task FilterTripsNullFieldsTest()
        {
            var trips = await _service.FilterTrips(new FilterTrips { });
            Assert.IsNotNull(trips);
            foreach (var trip in trips)
            {
                Assert.IsNotNull(trip.Title);
                Assert.IsNotNull(trip.Description);
            }
        }

        [Test]
        public async Task FilterTripsAllFieldsTest()
        {
            var trips = await _service.FilterTrips(new FilterTrips
            {
                TripStatus = 1,
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

            foreach (var trip in trips)
            {
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
            foreach (var entertainment in entertainments)
            {
                Assert.IsNotNull(entertainment.Title);
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
                Type = (int)EntertainmentType.Event

            });
            Assert.IsNotNull(entertainments);
            foreach (var entertainment in entertainments)
            {
                Assert.True(entertainment.Title.Contains("title"));
                Assert.AreEqual(entertainment.Type, EntertainmentType.Event);
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
