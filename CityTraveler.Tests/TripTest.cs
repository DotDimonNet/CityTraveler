using CityTraveler.Domain.Entities;
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
        public async Task GetTripByIdTest()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trip = service.GetTripById(tripId);
            Assert.IsNotNull(trip);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTripsTest()
        {
            var trip = ArrangeTests.ApplicationContext.Trips;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trips = service.GetTrips();
            Assert.IsNotNull(trips);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTripsByName()
        {
            var tripTitle = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Title;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trip = service.GetTripsByName(tripTitle);
            Assert.IsNotNull(trip);
            

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task AddNewTripTest()
        {
            var trip = new TripModel();        
            var service = new TripService(ArrangeTests.ApplicationContext);
            var newTrip = await service.AddNewTripAsync(trip);
            Assert.True(newTrip);

            ArrangeTests.UserManagerMock
               .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
         
        [Test]
        public async Task DeleteTripTest()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var deletedTrip = await  service.DeleteTripAsync(tripId);
            Assert.True(deletedTrip);

            ArrangeTests.UserManagerMock
               .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTripsByDateTest()
        {
            var tripDate = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().TripStart;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trip = service.GetTripsByDate(tripDate);
            Assert.IsNotNull(trip);
            
            ArrangeTests.UserManagerMock
              .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTripsByAverageRatingTest()
        {
            var tripRating = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().AverageRating;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trip = service.GetTripsByAverageRating(tripRating);
            Assert.IsNotNull(trip);

            ArrangeTests.UserManagerMock
              .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }     

        [Test]
        public async Task GetTripsByEntertainmentTest()
        {
            var entertainmentId = ArrangeTests.ApplicationContext.Entertaiments.FirstOrDefault().Id;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trips = service.GetTripsByEntertainmentAsync(entertainmentId);
            Assert.IsNotNull(trips);

            ArrangeTests.UserManagerMock
             .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTripsByEntertainmentNameTest()
        {
            var entertainmentName = ArrangeTests.ApplicationContext.Entertaiments.FirstOrDefault().Title;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trips = service.GetTripsByEntartainmentName(entertainmentName);
            Assert.IsNotNull(trips);

            ArrangeTests.UserManagerMock
             .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GettripsByAverageRatingtest()
        {
            var tripaveragerating = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().AverageRating;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trips = service.GetTripsByAverageRating(tripaveragerating);
            Assert.IsNotNull(trips);

            ArrangeTests.UserManagerMock
             .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTripsByStatusTest()
        {
            var tripStatus = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().TripStatus;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trips = service.GetTripsByStatus(tripStatus);
            Assert.IsNotNull(trips);

            ArrangeTests.UserManagerMock
            .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task UpdatetripStatusAsyncTest()
        {
            var tripid = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var newtripSattus = ArrangeTests.ApplicationContext.TripStatuses.FirstOrDefault(x=>x.Name=="Passed");
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trip = await service.UpdateTripSatusAsync(tripid, newtripSattus);
            Assert.True(trip);

            ArrangeTests.UserManagerMock
            .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        } 

    }
}

