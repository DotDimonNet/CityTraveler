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
    public class HistoryTest
    {
        private Mock<ILogger<HistoryService>> _loggerMock;
        private HistoryService _service;
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<HistoryService>());
            _service = new HistoryService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, _loggerMock.Object);
        }
        [Test]
        public async Task GetUserLastTripTest()
        {
            var userId = ArrangeTests.ApplicationContext.Users.FirstOrDefault(x => x.Trips.Count > 0).Id;

            var userRating = await _service.GetUserLastTrip(userId);
            Assert.IsNotNull(userRating);
        }
        [Test]
        public async Task GetUserLastReviewTest()
        {
            var userId = ArrangeTests.ApplicationContext.Users.FirstOrDefault(x => x.Reviews.Count > 0).Id;

            var userRating = await _service.GetUserLastReview(userId);
            Assert.IsNotNull(userRating);
        }
    }
}
