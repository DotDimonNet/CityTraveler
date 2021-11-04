using CityTraveler.Domain.DTO;
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

        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<EntertainmentService>());
        }

        [Test]
        public async Task GetEntertainmentByIdTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments.FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var testEntertainment = await service.GetEntertainmentById(entertainment.Id);

            Assert.IsNotNull(entertainment);
            Assert.IsNotNull(testEntertainment);
            Assert.AreEqual(entertainment, testEntertainment);
        }

        [Test]
        public void GetEntertainmentByCoordinatesTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x=>x.Address.Coordinates!=null);
            var coordinateDto = new CoordinatesDTO()
            {
                Latitude = entertainment.Address.Coordinates.Latitude,
                Longitude = entertainment.Address.Coordinates.Longitude
            };
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var testEntertainment =  service.GetEntertainmentsByCoordinates(coordinateDto);

            /*Assert.IsNotNull(entertainment);
            Assert.IsNotNull(testEntertainment);
            Assert.AreEqual(entertainment, testEntertainment);*/
        }

        [Test]
        public void GetEntertainmentsByStreetTest()
        {
            var street = ArrangeTests.ApplicationContext.Streets
                .FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var entertainments = service.GetEntertainmentsByStreet(street.Title);

            Assert.IsNotNull(street);
            Assert.IsNotNull(entertainments);
            foreach (var entertainment in entertainments)
            {
                Assert.AreEqual(entertainment.Address.Street, street);
            }
        }

        [Test]
        public void GetEntertainmentsByStreetTitleTest()
        {
            var streetTitle = ArrangeTests.ApplicationContext.Streets
                .Select(x=>x.Title).FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var entertainments = service.GetEntertainmentsByStreet(streetTitle);

            Assert.IsNotNull(streetTitle);
            Assert.IsNotNull(entertainments);
            foreach (var entertainment in entertainments)
            {
                Assert.AreEqual(entertainment.Address.Street.Title, streetTitle);
            }
        }
        
        [Test]
        public void GetEntertainmentByTitleTest()
        {
            var entertainments = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x=>x.Title.Contains("2")).ToList();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var testEntertainments = service.GetEntertainmentsByTitle("2").ToList();

            Assert.IsNotNull(entertainments);
            Assert.IsNotNull(testEntertainments);
            Assert.AreEqual(testEntertainments, entertainments);*/
        }

        [Test]
        public void GetEntertainmentsTest()
        {
            var entertainmentsIds = ArrangeTests.ApplicationContext.Entertaiments
                .Select(x=>x.Id);
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var entertainments = service.GetEntertainments(entertainmentsIds);

            Assert.IsNotNull(entertainmentsIds);
            Assert.IsNotNull(entertainments);
            Assert.AreEqual(entertainments.Count(), entertainmentsIds.Count());
        }

        [Test]
        public async Task GetEntertainmentByAddressTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault();
            var addressDto = new AddressGetDTO()
            {
                HouseNumber = address.HouseNumber,
                ApartmentNumber = address.ApartmentNumber,
            }; 
            var service = new EntertainmentService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerEntertainment);

            var entertainment = await service.GetEntertainmentByAddress(addressDto);

            Assert.IsNotNull(addressDto);
            Assert.IsNotNull(entertainment);
            Assert.AreEqual(entertainment, address.Entertaiment);*/
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