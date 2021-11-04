using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
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
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task DeleteTripTest()
        {
            var trip = ArrangeTests.ApplicationContext.Trips.FirstOrDefault();
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);

            var isRemoved = await service.DeleteTripAsync(trip.Id);

            Assert.IsTrue(isRemoved);
            Assert.IsFalse(ArrangeTests.ApplicationContext.Trips.Contains(trip));
        }

        [Test]
        public async Task AddTripAsyncTest()
        {
            var tripDTO = new AddNewTripDTO() { Title="Trip Title 111", Description="Trip Description 111" };
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
            var isAdded = await service.AddNewTripAsync(tripDTO);

            Assert.IsTrue(isAdded);
        }

        [Test]
        public async Task AdfDefaultTripTest()
        {
            var defaultTripDTO = new DefaultTripDTO() { Title = "Default Trip Title", Description = "Default trip Description" };
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
            var isAdded = await service.AddDefaultTrip(defaultTripDTO);

            Assert.IsTrue(isAdded);
        } 

        [Test]
        public async Task GetDefaultTripsTets()
        {
            var service = new TripService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper);
            var defautTrips = service.GetDefaultTrips();

            Assert.IsNotNull(defautTrips);
            Assert.IsNotEmpty(defautTrips);
        }
    }
}

