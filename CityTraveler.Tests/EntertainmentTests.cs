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
        public void GetEntertainmentByTitleTest()
        {
            var entertainments = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x => x.Title.Contains("2")).ToList();

            var allTestEntertainments = service.GetEntertainmentsByTitle("2").ToList();
            var landscapeTestEntertainments = service.GetEntertainmentsByTitle("2", 1);
            var institutionTestEntertainments = service.GetEntertainmentsByTitle("2", 2);
            var eventlTestEntertainments = service.GetEntertainmentsByTitle("2", 3);
            var emptyEntertainments = service.GetEntertainmentsByTitle("2", 4);

            Assert.AreEqual(allTestEntertainments, entertainments);
            Assert.IsFalse(emptyEntertainments.Any());
            foreach (var entertainment in allTestEntertainments)
            {
                Assert.IsTrue(landscapeTestEntertainments.Contains(entertainment) && entertainment.Type == EntertainmentType.Landscape
                    || institutionTestEntertainments.Contains(entertainment) && entertainment.Type == EntertainmentType.Institution
                    || eventlTestEntertainments.Contains(entertainment) && entertainment.Type == EntertainmentType.Event);
            }
        }

        [Test]
        public async Task GetEntertainmentByIdTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments.FirstOrDefault();

            var testEntertainment = await service.GetEntertainmentByIdAsync(entertainment.Id);

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

            var entertainment = await service.GetEntertainmentByAddressAsync(addressDto);

            Assert.IsNotNull(addressDto);
            Assert.IsNotNull(entertainment);
            Assert.AreEqual(entertainment, address.Entertaiment);
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