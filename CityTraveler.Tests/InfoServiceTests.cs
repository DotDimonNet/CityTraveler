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
    public class InfoServiceTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]

        public async Task GetMostPopularUserEntertaimentTripsTests()
        {
            var userModel = ArrangeTests.ApplicationContext.Users
                .LastOrDefault(x => x.Trips.Count > 0);
            var mostPopularUserEntertaimentInTrips = ArrangeTests.ApplicationContext.Users
                 .FirstOrDefault(x => x.Id == userModel.Id).Trips
                 .SelectMany(x => x.Entertaiment)
                 .OrderByDescending(x => x.Trips.Count)
                 .FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);

            var entertaiment = await service.GetMostPopularEntertaimentInTrips(userModel.Id);

            Assert.IsNotNull(entertaiment);
            Assert.AreEqual(entertaiment.Id, mostPopularUserEntertaimentInTrips.Id);
            Assert.AreEqual(entertaiment.Title, mostPopularUserEntertaimentInTrips.Title);
        }

        [Test]
        public async Task GetMostPopularEntertaimentTripsTests()
        {
            var mostPopularEntertainment = ArrangeTests.ApplicationContext.Users
                .SelectMany(x => x.Trips)
                .SelectMany(x => x.Entertaiment)
                .OrderByDescending(x => x.Trips.Count).FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var entertaiment = await service.GetMostPopularEntertaimentInTrips();

            Assert.IsNotNull(entertaiment);
            Assert.AreEqual(mostPopularEntertainment.Id, entertaiment.Id);
            Assert.AreEqual(mostPopularEntertainment.Title, entertaiment.Title);
            Assert.AreEqual(mostPopularEntertainment.Trips.Count, entertaiment.Trips.Count);
        }

        [Test]
        public async Task GetTripByMaxChoiceOfUsersTest()
        {
            var trip = ArrangeTests.ApplicationContext.Trips
                .OrderByDescending(x => x.Users.Count).FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripFromMethod = await service.GetTripByMaxChoiceOfUsers();

            Assert.AreEqual(tripFromMethod.Id, trip.Id);
            Assert.AreSame(trip, tripFromMethod);
            Assert.IsNotNull(tripFromMethod);
         }

        [Test]
        public async Task GetUserReviewByMaxCommentsTest()
        {
            var user = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault(x => x.Reviews.Count > 0);
            var userReview = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault(x => x.Id == user.Id).Reviews
                .OrderByDescending(x => x.Comments.Count)
                .FirstOrDefault();

            var serviceInfoService = new InfoService(ArrangeTests.ApplicationContext);
            var review = await serviceInfoService.GetReviewByMaxComments(user.Id);

            Assert.AreEqual(review.Id, userReview.Id);
            Assert.AreEqual(userReview.Comments.Count, review.Comments.Count);
            Assert.IsNotNull(review);
        }

        [Test]
        public async Task GetReviewByMaxCommentTest()
        {
            var reviewExpected = ArrangeTests.ApplicationContext.Users
                .Where(x => x.Reviews.Count > 0)
                .SelectMany(x => x.Reviews)
                .OrderByDescending(x => x.Comments.Count)
                .FirstOrDefault();
            var serviceInfoservice = new InfoService(ArrangeTests.ApplicationContext);
            var reviewActual = await serviceInfoservice.GetReviewByMaxComments();

            Assert.AreEqual(reviewExpected.Id, reviewActual.Id);
            Assert.AreEqual(reviewExpected.Comments.Count, reviewActual.Comments.Count);
            Assert.IsNotNull(reviewActual);
        }

        [Test]

        public async Task GetTripByMaxReviewTest()
        {
            var tripExpected = ArrangeTests.ApplicationContext.Users
                .SelectMany(x => x.Trips)
                .OrderByDescending(x => x.Reviews.Count > 0).FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripActual = await service.GetTripByMaxReview();

            Assert.AreEqual(tripExpected.Title, tripActual.Title);
            Assert.AreEqual(tripExpected.Reviews.Count, tripActual.Reviews.Count);
            Assert.AreEqual(tripExpected.Id, tripActual.Id);
            Assert.IsNotNull(tripActual);
        }

        [Test]
        public async Task GetUserTripByMaxReviewTest()
        {
            var user = ArrangeTests.ApplicationContext.Users
                .LastOrDefault();
            var tripExpected = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault(x => x.Id == user.Id)
                .Trips
                .OrderByDescending(x => x.Reviews.Count)
                .FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripActual = await service.GetTripByMaxReview(user.Id);

            Assert.AreEqual(tripExpected.Reviews.Count, tripActual.Reviews.Count);
            Assert.AreEqual(tripExpected.Id, tripActual.Id);
            Assert.IsNotNull(tripActual);
        }

        [Test]

        public void GetlastTripByPeriodTest()
        {
            var startDateTime = DateTime.Now;
            var endDateTime = DateTime.Now.AddHours(4);
            var tripsExpected = ArrangeTests.ApplicationContext
                .Trips.Where(x => x.TripStart > startDateTime && x.TripEnd < endDateTime);
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripsActual = service.GetLastTripsByPeriod(startDateTime, endDateTime);

            Assert.IsNotNull(tripsActual);
            Assert.AreEqual(tripsExpected.Count(), tripsActual.Count());


            foreach (var tripactual in tripsActual)
            {
                Assert.AreEqual(tripsExpected.Select(x => x.TripStart), tripsActual.Select(x => x.TripStart));
                Assert.AreEqual(tripsExpected.Select(x => x.TripEnd), tripsActual.Select(x => x.TripEnd));
                Assert.AreEqual(tripsExpected.Select(x => x.Id), tripsActual.Select(x => x.Id));
            }
        }

        [Test]
        public async Task GetRegisteredUsersByPeriodTest()
        {
            var startOfPeriod = DateTime.Now;
            var endOfPeriod = DateTime.Now.AddMilliseconds(4);
            var usersExpected = ArrangeTests.ApplicationContext.Users
                .Count(x => x.Profile.Created > startOfPeriod && x.Profile.Created < endOfPeriod);
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var usersActual = await service.GetRegisteredUsersByPeriod(startOfPeriod, endOfPeriod);

            Assert.AreEqual(usersExpected, usersActual);
            Assert.IsNotEmpty(usersActual.ToString(), usersExpected.ToString());

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }

        /*[Test]

        public async Task GetMostlyUsedTemplateTest()
        {
            var templateId = ArrangeTests.ApplicationContext.Trips
                .Select(x => x.TemplateId).GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
            var templateExpected = ArrangeTests.ApplicationContext.Trips.FirstOrDefault(x => x.TemplateId == templateId);

            var service = new InfoService(ArrangeTests.ApplicationContext);
            var templateActual = await service.GetMostlyUsedTemplate();

            Assert.IsNotNull(templateActual);
            Assert.AreEqual(templateExpected, templateActual);
            Assert.AreEqual(templateExpected.Id, templateActual.Id);

            ArrangeTests.UserManagerMock
                .Verify(x => x.CreateAsync(It.IsAny<ApplicationUserModel>(), It.IsAny<string>()), Times.Once);
        }*/

        [Test]

        public void GetUsersCountTripsDateRangeTest()
        {
            var startOfPeriod = DateTime.Now;
            var endOfPeriod = DateTime.Now.AddMilliseconds(4);
            var usersCreatedTripByperiodExpected = ArrangeTests.ApplicationContext.Trips
                .Where(x => x.TripStart > startOfPeriod && x.TripEnd < endOfPeriod)
                .SelectMany(x => x.Users).Count();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var usersCreatedTripsByPeriodActual = service.GetUsersCountTripsDateRange(startOfPeriod, endOfPeriod);

            Assert.AreEqual(usersCreatedTripByperiodExpected, usersCreatedTripsByPeriodActual);
            Assert.IsNotNull(usersCreatedTripsByPeriodActual);
        }

        [Test]

        public async Task GetLongestTripTest()
        {
            var tripExpected = ArrangeTests.ApplicationContext.Trips
                .OrderByDescending(x => x.RealSpent).FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripActual = await service.GetLongestTrip();

            Assert.IsNotNull(tripActual);
            Assert.AreEqual(tripExpected.Id, tripActual.Id);
        }

        [Test]

        public async Task GetShortestTripTest()
        {
            var tripExpected = ArrangeTests.ApplicationContext.Trips
                .OrderBy(x => x.RealSpent).FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripActual = await service.GetShortestTrip();

            Assert.IsNotNull(tripActual);
            Assert.AreEqual(tripExpected.Id, tripActual.Id);
        }

        [Test]
        public void GetTripsCreatedByPeriod()

        {
            var startOfPeriod = DateTime.Now;
            var endOfPeriod = DateTime.Now.AddMilliseconds(4);
            var tripsCreatedExpected = ArrangeTests.ApplicationContext.Trips
                .Where(x => x.TripStart > startOfPeriod && x.TripEnd < endOfPeriod).Count();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripscreatedActual = service.GetTripsCreatedByPeriod(startOfPeriod, endOfPeriod);

            Assert.IsNotNull(tripscreatedActual);
            Assert.AreEqual(tripsCreatedExpected, tripscreatedActual);

        }








    }

}
