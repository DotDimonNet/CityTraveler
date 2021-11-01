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
            var trips = service.GetTripsByName(tripTitle);
            Assert.IsNotNull(trips);
            foreach (var trip in trips)
            {
                Assert.AreEqual(trip.Title, tripTitle);
            }

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task AddNewTripTest()
        {
            var trip = new TripModel();        
            var service = new TripService(ArrangeTests.ApplicationContext);
            Assert.ThrowsAsync<Exception>(async () =>
            {
                var newTrip = await service.AddNewTripAsync(trip);
            });
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

        [Test]
        public async Task GetTripsByPriceTest()
        {
            var tripPrice = ArrangeTests.ApplicationContext.Prices.FirstOrDefault().Value;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trips = service.GetTripsByPrice(tripPrice);
            Assert.IsNotNull(trips);
            foreach ( var trip in trips)
            {
                Assert.AreEqual(trip.Price.Value, tripPrice);
            }

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task UpdateTripTitleAsyncTest()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var newTripTile = "NewTripTitle";
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trip = await service.UpdateTripTitleAsync(tripId,newTripTile);
            Assert.True(trip);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task UpdateTripDescriptionTest()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var newtripDescription = "New trip Description";
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trip = await service.UpdateTripDescriptionAsync(tripId, newtripDescription);
            Assert.True(trip);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task AddEntertainmentToTripTets()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var trip = ArrangeTests.ApplicationContext.Trips.FirstOrDefault(x => x.Id == tripId);
            EntertaimentModel newEntertainment = new EntertaimentModel { Title = "New Entertainment Title" };
            
            var service = new TripService(ArrangeTests.ApplicationContext);
            var isAdded = await service.AddEntertainmetToTripAsync(tripId, newEntertainment);
            Assert.IsNotNull(isAdded);
            Assert.True(isAdded);
            Assert.True(trip.Entertaiment.Contains(newEntertainment));

            ArrangeTests.UserManagerMock
               .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        } 

        [Test]
        public async Task DeleteEntertainmentFromTripTest()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var trip = ArrangeTests.ApplicationContext.Trips.FirstOrDefault(x=>x.Id==tripId);
            var entertainment = await  ArrangeTests.ApplicationContext.Entertaiments.FirstOrDefaultAsync();
            var service = new TripService(ArrangeTests.ApplicationContext);
            var removedEntertainment = await service.DeleteEntertainmentFromTrip(tripId, entertainment);
            Assert.True(removedEntertainment);
            Assert.False(trip.Entertaiment.Contains(entertainment));

            ArrangeTests.UserManagerMock
               .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
        [Test]
        public async Task GetTripsByTagTest()
        {
            var tripTagString = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().TagSting;
            var service = new TripService(ArrangeTests.ApplicationContext);
            var trips = service.GetTripsByTag(tripTagString);
            Assert.IsNotNull(trips);
     
            ArrangeTests.UserManagerMock
              .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task SetTripsAsDefaultTest()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var trip = ArrangeTests.ApplicationContext.Trips.FirstOrDefault(x=>x.Id==tripId);
            var service = new TripService(ArrangeTests.ApplicationContext);
            var settedTripAsDafault = await service.SetTripAsDefault(tripId);
            Assert.True(settedTripAsDafault);
            Assert.AreEqual(settedTripAsDafault, trip.DafaultTrip == true);

            ArrangeTests.UserManagerMock
             .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task RemoveTripFromDefaultTest()
        {
            var tripId = ArrangeTests.ApplicationContext.Trips.FirstOrDefault().Id;
            var trip = ArrangeTests.ApplicationContext.Trips.FirstOrDefault(x=>x.Id==tripId);
            var service = new TripService(ArrangeTests.ApplicationContext);
            var removedReipFromDefault = await service.RemooveTripFromDefault(tripId);
            Assert.True(removedReipFromDefault);
            Assert.AreEqual(removedReipFromDefault, trip.DafaultTrip==false);

            ArrangeTests.UserManagerMock
            .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }
    }
}

