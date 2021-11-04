using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Logging.Abstractions;
>>>>>>> 6694f9dfc711096bf70aee411350a3f1b232939e
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Tests
{
    class TripTest
    {
        private Mock<ILogger<TripService>> _loggerMock;

        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
<<<<<<< HEAD
            
=======
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<TripService>());
>>>>>>> 6694f9dfc711096bf70aee411350a3f1b232939e
        }
        [Test]
        public async Task DeleteTripTest()
        {
            var trip = ArrangeTests.ApplicationContext.Trips.FirstOrDefault();
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerTrip);

            var isRemoved = await service.DeleteTripAsync(trip.Id);

            Assert.IsTrue(isRemoved);
            Assert.IsFalse(ArrangeTests.ApplicationContext.Trips.Contains(trip));
        }

        [Test]
        public async Task GetDefaultTripByIdTest()
        {
            var trip = ArrangeTests.ApplicationContext.Trips.First();

            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerTrip);
            var model = ArrangeTests.TestMapper.Map<TripModel, DefaultTripDTO>(trip);

            var testTrip = service.GetDefaultTripById(trip.Id);
            Assert.IsNotNull(model);
            Assert.IsNotNull(testTrip);
            Assert.AreEqual(model, testTrip);
        }

        [Test]
        public async Task AddTripAsyncTest()
        {
<<<<<<< HEAD
            var tripDTO = new AddNewTripDTO() { Title = "Trip Title 111", Description = "Trip Description 111" };

            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
=======
            var tripDTO = new AddNewTripDTO() { Title="Trip Title 111", Description="Trip Description 111",};
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerTrip);
>>>>>>> 6694f9dfc711096bf70aee411350a3f1b232939e
            var isAdded = await service.AddNewTripAsync(tripDTO);

            Assert.IsTrue(isAdded);
        }

        [Test]
        public async Task AdfDefaultTripTest()
        {
            var defaultTripDTO = new DefaultTripDTO() { Title = "Default Trip Title", Description = "Default trip Description" };
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerTrip);
            var isAdded = await service.AddDefaultTrip(defaultTripDTO);

            Assert.IsTrue(isAdded);
        }

        [Test]
        public async Task GetDefaultTripsTets()
        {
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, ArrangeTests.LoggerTrip);
            var defautTrips = service.GetDefaultTrips();

            Assert.IsNotNull(defautTrips);
            Assert.IsNotEmpty(defautTrips);
        }
    }
}

