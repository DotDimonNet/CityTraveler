using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Enums;
using CityTraveler.Services;
using CityTraveler.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;

namespace CityTraveler.Tests
{
    public class EntertainmentTests
    {
        private Mock<ILogger<EntertainmentService>> _loggerMock;
        EntertainmentService service;

        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<EntertainmentService>());
            service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);
        }

        [Test]
        public void GetAllDTOTest()
        {
            var allEntertainments = service.GetAllDTO();
            var landscapeEntertainments = service.GetAllDTO(1);
            var institutionEntertainments = service.GetAllDTO(2);
            var eventEntertainments = service.GetAllDTO(3);
            //var emptyEntertainments = service.GetAllDTO(4);

            Assert.IsTrue(ArrangeTests.ApplicationContext.Entertaiments.Count() > 0);
            Assert.AreEqual(allEntertainments.Count(), ArrangeTests.ApplicationContext.Entertaiments.Count());
            //Assert.IsFalse(emptyEntertainments.Any());

            foreach (var entertainment in landscapeEntertainments)
            {
                Assert.IsTrue(entertainment.Type == "Landscape");
                Assert.AreEqual(entertainment, allEntertainments.First(x => x.Id == entertainment.Id));
            }

            foreach (var entertainment in institutionEntertainments)
            {
                Assert.IsTrue(entertainment.Type == "Institution");
                Assert.AreEqual(entertainment, allEntertainments.First(x => x.Id == entertainment.Id));
            }

            foreach (var entertainment in eventEntertainments)
            {
                Assert.IsTrue(entertainment.Type == "Event");
                Assert.AreEqual(entertainment, allEntertainments.First(x => x.Id == entertainment.Id));
            }
        }

        [Test]
        public void GetAverageRatingTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var averageRating = service.GetAverageRating(entertainment);

            Assert.IsNotNull(entertainment);
            Assert.IsNotNull(averageRating);
        }
    }
}