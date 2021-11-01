using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Tests
{
    public class EventServiceTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task GetEventByIdTest()
        {
            var entertainment = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x=>x.Type==EntertainmentType.Event);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var testEntertainment = await service.GetEventById(entertainment.Id);

            Assert.IsNotNull(entertainment);
            Assert.AreEqual(testEntertainment, entertainment);
        }

        [Test]
        public async Task GetEventByCoordinatesTest()
        {
            var realEvent = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Type == EntertainmentType.Event && x.Address.Coordinates!=null);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var testEvent = await service.GetEventByCoordinates(realEvent.Address.Coordinates);

            Assert.IsNotNull(realEvent);
            Assert.AreEqual(realEvent, testEvent);
        }

        [Test]
        public void GetEventsByStreetTest()
        {
            var street = ArrangeTests.ApplicationContext.Streets
                .FirstOrDefault();
            var service = new EventService(ArrangeTests.ApplicationContext);

            var events = service.GetEventsByStreet(street);

            Assert.IsNotNull(events);

            foreach (var item in events)
            {
                Assert.AreEqual(item.Address.Street, street);
            }
        }

        [Test]
        public void GetEventsByStreetTitleTest()
        {
            var streetTitle = ArrangeTests.ApplicationContext.Streets
                .Select(x => x.Title).FirstOrDefault();
            var service = new EventService(ArrangeTests.ApplicationContext);

            var events = service.GetEventsByStreet(streetTitle);

            Assert.IsNotNull(events);

            foreach (var item in events)
            {
                Assert.AreEqual(item.Address.Street.Title, streetTitle);
            }
        }

        [Test]
        public void GetEventByTitleTest()
        {
            var realEvents = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x=>x.Type==EntertainmentType.Event && x.Title.Contains("2")).ToList();
            var service = new EventService(ArrangeTests.ApplicationContext);

            var testEvents = service.GetEventByTitle("2").ToList();

            Assert.IsNotNull(realEvents);
            Assert.AreEqual(testEvents, realEvents);
        }

        [Test]
        public void GetEventsTest()
        {
            var realEvents = ArrangeTests.ApplicationContext.Entertaiments.Where(x=>x.Type==EntertainmentType.Event);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var testEvents = service.GetEvents(realEvents.Select(x=>x.Id));

            Assert.IsNotNull(testEvents);
            Assert.AreEqual(testEvents.Count(), realEvents.Count());
        }

        [Test]
        public async Task GetEventByAddressTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault(x=>x.Entertaiment.Type==EntertainmentType.Event);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var testEvent = await service.GetEventByAddress(address);

            Assert.IsNotNull(testEvent);
            Assert.AreEqual(testEvent, address.Entertaiment);
        }

        [Test]
        public async Task GetEventByAddressStringTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault(x=>x.Entertaiment.Type==EntertainmentType.Event);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var testEvent = await service.GetEventByAddress(address.HouseNumber,
                address.ApartmentNumber, address.Street.Title);

            Assert.IsNotNull(testEvent);
            Assert.AreEqual(testEvent, address.Entertaiment);
        }
    }
}