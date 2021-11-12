using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;

namespace CityTraveler.Tests
{
    
    public class StatisticServiceTest
    {
        private Mock<ILogger<StatisticService>> _loggerMock;
        private StatisticService _service;
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<StatisticService>());
            _service = new StatisticService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, _loggerMock.Object, new UserManagementService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.SetupTestLogger(new NullLogger<UserManagementService>()).Object));
        }

        [Test]
        public async Task GetAverageRatingUserTripTest()
        {
            var userId = ArrangeTests.ApplicationContext.Users.FirstOrDefault(x => x.Trips.Count > 0).Id;

            var userRating = await _service.GetAverageRatingUserTrip(userId);
            Assert.IsNotNull(userRating);

        }
        [Test]
        public async Task GetCountPassedUserTripTest()
        {
            var userId = ArrangeTests.ApplicationContext.UserProfiles
                .FirstOrDefault().User.UserId;

            var userPassTrip = await _service.GetCountPassedUserTrip(userId);
            Assert.IsNotNull(userPassTrip);
        }
        [Test]
        public async Task QuantityPassEntertaimentTest()
        {
            var userId = ArrangeTests.ApplicationContext.UserProfiles
                .FirstOrDefault().User.UserId;

            var userNumberEntertaiments = await _service.QuantityPassEntertaiment(userId);
            Assert.IsNotNull(userNumberEntertaiments);
        }
        [Test]
        public async Task GetAverageAgeUser()
        {
            var Age = await _service.GetAverageAgeUser();
            Assert.IsNotNull(Age);
        }

    }
}
