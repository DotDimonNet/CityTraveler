using CityTraveler.Domain.Entities;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Tests
{
    public class EntertainmentTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task GetEntertainmentByIdTest()
        {
            var entertainmentId = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault().Id;
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var entertainment = await service.GetEntertainmentById(entertainmentId);
            Assert.IsNotNull(entertainment);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
    }
}