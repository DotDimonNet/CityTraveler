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

namespace CityTraveler.Tests
{
    public class AdminPanelServiceTest
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task AdminFilterUsersTest()
        {
            var filter = new FilterAdminUser();
            var service = new AdminPanelService(ArrangeTests.ApplicationContext);

            var Users = await service.AdminFilterUsers(filter);
            Assert.IsNotNull(Users);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
    }
}
