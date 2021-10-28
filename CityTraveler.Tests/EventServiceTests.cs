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
            var entertainmentId = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x=>x.Type==EntertainmentType.Event).Id;
            var service = new EventService(ArrangeTests.ApplicationContext);

            var entertainment = await service.GetEventById(entertainmentId);
            Assert.IsNotNull(entertainment);
        }

        [Test]
        public async Task GetEventByCoordinatesTest()
        {
            var eventCoordinates = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Type == EntertainmentType.Event).Address.Coordinates;
            var service = new EventService(ArrangeTests.ApplicationContext);

            var realEvent = await service.GetEventByCoordinates(eventCoordinates);
            Assert.IsNotNull(realEvent);
            Assert.AreEqual(realEvent.Type, EntertainmentType.Event);
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
        public async Task GetEventByTitleTest()
        {
            var randomEvent = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x=>x.Type==EntertainmentType.Event);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var realEvent = await service.GetEventByTitle(randomEvent.Title);
            Assert.IsNotNull(realEvent);
            Assert.AreEqual(realEvent, randomEvent);
        }

        [Test]
        public void GetEventsTest()
        {
            var eventsIds = ArrangeTests.ApplicationContext.Entertaiments.Where(x=>x.Type==EntertainmentType.Event)
                .Select(x => x.Id);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var events = service.GetEvents(eventsIds);
            Assert.IsNotNull(events);
            Assert.AreEqual(events.Count(), eventsIds.Count());
        }

        [Test]
        public async Task GetEventByAddressTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault(x=>x.Entertaiment.Type==EntertainmentType.Event);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var realEvent = await service.GetEventByAddress(address);
            Assert.IsNotNull(realEvent);
            Assert.AreEqual(realEvent, address.Entertaiment);
        }

        [Test]
        public async Task GetEventByAddressStringTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault(x=>x.Entertaiment.Type==EntertainmentType.Event);
            var service = new EventService(ArrangeTests.ApplicationContext);

            var realEvent = await service.GetEventByAddress(address.HouseNumber,
                address.ApartmentNumber, address.Street.Title);
            Assert.IsNotNull(realEvent);
            Assert.AreEqual(realEvent, address.Entertaiment);
        }
    }
}