using CityTraveler.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using CityTraveler.Domain.Filters.Admin;

namespace CityTraveler.Tests
{
    public class AdminPanelServiceTest
    {
        private Mock<ILogger<AdminPanelService>> _loggerMock;

        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<AdminPanelService>());
        }

        [Test]
        public async Task FilterUsersTest()
        {
            var filter = new FilterAdminUser();
            var service = new AdminPanelService(ArrangeTests.ApplicationContext,
                                                ArrangeTests.TestMapper,
                                                _loggerMock.Object);

            var Users = await service.FilterUsers(filter);
            Assert.IsNotNull(Users);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
        [Test]
        public async Task FilterTripTest()
        {
            var filter = new FilterAdminTrip();
            var service = new AdminPanelService(ArrangeTests.ApplicationContext,
                                                ArrangeTests.TestMapper,
                                                _loggerMock.Object);

            var Users = await service.FilterTrips(filter);
            Assert.IsNotNull(Users);
        }
        [Test]
        public async Task FindAdressStreetsTest()
        {
            var filter = new FilterAdminStreet();
            var service = new AdminPanelService(ArrangeTests.ApplicationContext,
                                                ArrangeTests.TestMapper,
                                                _loggerMock.Object);

            var Users = await service.FindAdressStreets(filter);
            Assert.IsNotNull(Users);
        }
        [Test]
        public async Task FilterEntertaimentsTest()
        {
            var filter = new FilterAdminEntertaiment();
            var service = new AdminPanelService(ArrangeTests.ApplicationContext,
                                                ArrangeTests.TestMapper,
                                                _loggerMock.Object);

            var Users = await service.FilterEntertaiments(filter);
            Assert.IsNotNull(Users);
        }
        [Test]
        public async Task FilterReviewsTest()
        {
            var filter = new FilterAdminReview();
            var service = new AdminPanelService(ArrangeTests.ApplicationContext,
                                                ArrangeTests.TestMapper,
                                                _loggerMock.Object);

            var Users = await service.FilterReview(filter);
            Assert.IsNotNull(Users);
        }

    }
}
