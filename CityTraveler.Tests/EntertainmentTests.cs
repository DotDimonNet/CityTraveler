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
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var testEntertainment = await service.GetEntertainmentById(entertainment.Id);

            Assert.IsNotNull(testEntertainment);
            Assert.AreEqual(entertainment, testEntertainment);
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetEntertainmentByCoordinatesTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x=>x.Address.Coordinates!=null);
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var testEntertainment = await service.GetEntertainmentByCoordinates(entertainment.Address.Coordinates);

            Assert.IsNotNull(testEntertainment);
            Assert.AreEqual(entertainment, testEntertainment);
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetEntertainmentsByStreetTest()
        {
            var street = ArrangeTests.ApplicationContext.Streets
                .FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var entertainments = service.GetEntertainmentsByStreet(street);

            Assert.IsNotNull(entertainments);
            foreach (var entertainment in entertainments)
            {
                Assert.AreEqual(entertainment.Address.Street, street);
            }
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetEntertainmentsByStreetTitleTest()
        {
            var streetTitle = ArrangeTests.ApplicationContext.Streets
                .Select(x=>x.Title).FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var entertainments = service.GetEntertainmentsByStreet(streetTitle);

            Assert.IsNotNull(entertainments);
            foreach (var entertainment in entertainments)
            {
                Assert.AreEqual(entertainment.Address.Street.Title, streetTitle);
            }
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public void GetEntertainmentByTitleTest()
        {
            var entertainments = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x=>x.Title.Contains("2")).ToList();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var testEntertainments = service.GetEntertainmentByTitle("2").ToList();

            Assert.IsNotNull(testEntertainments);
            Assert.AreEqual(testEntertainments, entertainments);
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetEntertainmentsTest()
        {
            var entertainmentsIds = ArrangeTests.ApplicationContext.Entertaiments
                .Select(x=>x.Id);
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var entertainments = service.GetEntertainments(entertainmentsIds);

            Assert.IsNotNull(entertainments);
            Assert.AreEqual(entertainments.Count(), entertainmentsIds.Count());
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetEntertainmentByAddressTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var entertainment = await service.GetEntertainmentByAddress(address);

            Assert.IsNotNull(entertainment);
            Assert.AreEqual(entertainment, address.Entertaiment);
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
        
        [Test]
        public async Task GetEntertainmentByAddressStringTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var entertainment = await service.GetEntertainmentByAddress(address.HouseNumber, 
                address.ApartmentNumber, address.Street.Title);

            Assert.IsNotNull(entertainment);
            Assert.AreEqual(entertainment, address.Entertaiment);
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void GetAverageRatingTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault();
            var service = new EntertainmentService(ArrangeTests.ApplicationContext);

            var averageRating = service.GetAverageRating(entertainment);

            Assert.IsNotNull(averageRating);
            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
    }
}