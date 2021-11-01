using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.DTO;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CityTraveler.Tests
{
    public class LandscapeServiceTest
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task GetLandscapeByIdTest()
        {
            var landscapes = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Type == EntertainmentType.Landskape);
            var service = new LandscapeService(ArrangeTests.ApplicationContext);

            var testLandscape = await service.GetLandscapeById(landscapes.Id);

            Assert.IsNotNull(landscapes);
            Assert.IsNotNull(testLandscape);
            Assert.AreEqual(testLandscape, landscapes);
        }

        [Test]
        public async Task GetLandscapeByCoordinatesTest()
        {
            var landscapes = ArrangeTests.ApplicationContext.Entertaiments
                .FirstOrDefault(x => x.Type == EntertainmentType.Landskape && x.Address.Coordinates != null);
            var coordinatesDto = new CoordinatesDTO()
            {
                Latitude = landscapes.Address.Coordinates.Latitude,
                Longitude = landscapes.Address.Coordinates.Longitude
            };
            var service = new LandscapeService(ArrangeTests.ApplicationContext);

            var testLandscape = await service.GetLandscapeByCoordinates(coordinatesDto);

            Assert.IsNotNull(landscapes);
            Assert.IsNotNull(landscapes);
            Assert.AreEqual(landscapes, testLandscape);
        }

        [Test]
        public void GetLandscapesByStreetTest()
        {
            var street = ArrangeTests.ApplicationContext.Streets
                .FirstOrDefault();
            var service = new LandscapeService(ArrangeTests.ApplicationContext);

            var landscapes = service.GetLandscapesByStreet(street.Title);

            Assert.IsNotNull(street);
            Assert.IsNotNull(landscapes);
            foreach (var item in landscapes)
            {
                Assert.AreEqual(item.Address.Street, street);
            }
        }

        [Test]
        public void GetLandscapesByStreetTitleTest()
        {
            var streetTitle = ArrangeTests.ApplicationContext.Streets
                .Select(x => x.Title).FirstOrDefault();
            var service = new LandscapeService(ArrangeTests.ApplicationContext);

            var landscapes = service.GetLandscapesByStreet(streetTitle);

            Assert.IsNotNull(streetTitle);
            Assert.IsNotNull(landscapes);
            foreach (var item in landscapes)
            {
                Assert.AreEqual(item.Address.Street.Title, streetTitle);
            }
        }

        [Test]
        public void GetLandscapeByTitleTest()
        {
            var Landscapes = ArrangeTests.ApplicationContext.Entertaiments
                .Where(x => x.Type == EntertainmentType.Landskape && x.Title.Contains("2")).ToList();
            var service = new LandscapeService(ArrangeTests.ApplicationContext);

            var testLandscapes = service.GetLandscapesByTitle("2").ToList();

            Assert.IsNotNull(Landscapes);
            Assert.IsNotNull(testLandscapes);
            Assert.AreEqual(testLandscapes, Landscapes);
        }

        [Test]
        public void GetLandscapesTest()
        {
            var landscapes = ArrangeTests.ApplicationContext.Entertaiments.Where(x => x.Type == EntertainmentType.Landskape);
            var service = new LandscapeService(ArrangeTests.ApplicationContext);

            var testLandscapes = service.GetLandscapes(landscapes.Select(x => x.Id));

            Assert.IsNotNull(landscapes);
            Assert.IsNotNull(landscapes);
            Assert.AreEqual(testLandscapes.Count(), landscapes.Count());
        }

        [Test]
        public async Task GetEventByAddressTest()
        {
            var address = ArrangeTests.ApplicationContext.Addresses
                .FirstOrDefault(x => x.Entertaiment.Type == EntertainmentType.Landskape);
            var addressDto = new AddressDTO()
            {
                ApartsmentNumber = address.ApartmentNumber,
                HouseNumber = address.HouseNumber,
                StreetTitle = address.Street.Title
            };
            var service = new LandscapeService(ArrangeTests.ApplicationContext);

            var testLandscape = await service.GetLandscapeByAddress(addressDto);

            Assert.IsNotNull(address);
            Assert.IsNotNull(testLandscape);
            Assert.AreEqual(testLandscape, address.Entertaiment);
        }
    }
}
