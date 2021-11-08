using CityTraveler.Domain.Entities;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Tests
{
    public class InfoServiceTests
    {
        private Mock<ILogger<InfoService>> _loggerMock;
        private InfoService service;

        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
            _loggerMock = ArrangeTests.SetupTestLogger(new NullLogger<InfoService>());
            service = new InfoService(ArrangeTests.ApplicationContext, ArrangeTests.TestMapper, _loggerMock.Object);
        }

        [Test]
        public async Task GetMostPopularUserEntertaimentTripsTests()
        {
            var userModel = ArrangeTests.ApplicationContext.Users
                .LastOrDefault(x => x.Trips.Count > 0);
            var mostPopularUserEntertaimentInTrips = ArrangeTests.ApplicationContext.Users
                 .FirstOrDefault(x => x.Id == userModel.Id).Trips
                 .SelectMany(x => x.Entertaiment)
                 .OrderByDescending(x => x.Trips.Count())
                 .FirstOrDefault();
            var entertaiment = await service.GetMostPopularEntertaimentInTripsAsync(userModel.Id);

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
                .OrderByDescending(x => x.Trips.Count()).FirstOrDefault();
            var entertaiment = await service.GetMostPopularEntertaimentInTripsAsync();

            Assert.IsNotNull(entertaiment);
            Assert.AreEqual(mostPopularEntertainment.Id, entertaiment.Id);
            Assert.AreEqual(mostPopularEntertainment.Title, entertaiment.Title);
            Assert.AreEqual(mostPopularEntertainment.Begin, entertaiment.Begin);
        }

        [Test]
        public async Task GetMostPopularTripTest()
        {
            var trip = ArrangeTests.ApplicationContext.Trips
                .OrderByDescending(x => x.Users.Count).FirstOrDefault();
            var tripFromMethod = await service.GetMostPopularTripAsync();

            Assert.AreEqual(tripFromMethod.Title, trip.Title);
            Assert.AreEqual(tripFromMethod.Users.Count(), trip.Users.Count());
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

            var review = await service.GetReviewByMaxCommentsAsync(user.Id);

            Assert.AreEqual(review.UserId, userReview.UserId);
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

            var reviewActual = await service.GetReviewByMaxCommentsAsync();

            Assert.AreEqual(reviewExpected.UserId, reviewActual.UserId);
            Assert.AreEqual(reviewExpected.Comments.Count, reviewActual.Comments.Count);
            Assert.IsNotNull(reviewActual);
        }

        [Test]
        public async Task GetTripByMaxReviewTest()
        {
            var tripExpected = ArrangeTests.ApplicationContext.Users
                .SelectMany(x => x.Trips)
                .OrderByDescending(x => x.Reviews.Count > 0).FirstOrDefault();

            var tripActual = await service.GetTripByMaxReviewAsync();

            Assert.AreEqual(tripExpected.Title, tripActual.Title);
            Assert.AreEqual(tripExpected.Reviews.Count, tripActual.Reviews.Count());
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

            var tripActual = await service.GetTripByMaxReviewAsync(user.Id);

            Assert.AreEqual(tripExpected.Reviews.Count(), tripActual.Reviews.Count());
            Assert.IsNotNull(tripActual);
        }

        [Test]
        public async Task GetlastTripByPeriodTest()
        {
            var startDateTime = DateTime.Now;
            var endDateTime = DateTime.Now.AddHours(4);
            var tripsExpected = ArrangeTests.ApplicationContext
                .Trips.Where(x => x.TripStart > startDateTime && x.TripEnd < endDateTime);

            var tripsActual = await service.GetLastTripsByPeriodAsync(startDateTime, endDateTime);

            Assert.IsNotNull(tripsActual);
            Assert.AreEqual(tripsExpected.Count(),tripsActual.Count());


            foreach (var tripactual in tripsActual)
            {
                Assert.AreEqual(tripsExpected.Select(x => x.TripStart), tripsActual.Select(x => x.TripStart));
                Assert.AreEqual(tripsExpected.Select(x => x.TripEnd), tripsActual.Select(x => x.TripEnd));
            }
        }

        [Test]
        public async Task GetRegisteredUsersByPeriodTest()
        {
            var startOfPeriod = DateTime.Now;
            var endOfPeriod = DateTime.Now.AddMilliseconds(4);
            var usersExpected = ArrangeTests.ApplicationContext.Users
                .Count(x => x.Profile.Created > startOfPeriod && x.Profile.Created < endOfPeriod);

            var usersActual = await service.GetRegisteredUsersByPeriodAsync(startOfPeriod, endOfPeriod);

            Assert.AreEqual(usersExpected, usersActual);
            Assert.IsNotEmpty(usersActual.ToString(), usersExpected.ToString());
        }

        [Test]
        public async Task GetMostlyUsedTemplatesTest()
        {
            var count = 2;
            var templateIds = ArrangeTests.ApplicationContext.Trips
                .Select(x => x.TemplateId)
                .GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .Select(x => x.Key)
                .Take(count);

            var templatesActual = await service.GetMostlyUsedTemplatesAsync(count);

            Assert.IsNotEmpty(templatesActual);
            //Assert.AreEqual(templatesActual.Count(), count);
        }    

        [Test]
        public async Task GetUsersCountTripsDateRangeTest()
        {
            var startOfPeriod = DateTime.Now;
            var endOfPeriod = DateTime.Now.AddMilliseconds(4);
            var usersCreatedTripByperiodExpected = ArrangeTests.ApplicationContext.Trips
                .Where(x => x.TripStart > startOfPeriod && x.TripEnd < endOfPeriod)
                .SelectMany(x => x.Users).Count();

            var usersCreatedTripsByPeriodActual = await service.GetUsersCountTripsDateRangeAsync(startOfPeriod, endOfPeriod);

            Assert.AreEqual(usersCreatedTripByperiodExpected, usersCreatedTripsByPeriodActual);
            Assert.IsNotNull(usersCreatedTripsByPeriodActual);
        }

        [Test]
        public async Task GetLongestTripTest()
        {
            var tripExpected = ArrangeTests.ApplicationContext.Trips
                .OrderByDescending(x => x.RealSpent).FirstOrDefault();

            var tripActual = await service.GetLongestTripAsync();

            Assert.IsNotNull(tripActual);
            Assert.AreEqual(tripExpected.Title, tripActual.Title);
        }

        [Test]
        public async Task GetShortestTripTest()
        {
            var tripExpected = ArrangeTests.ApplicationContext.Trips
                .OrderBy(x => x.RealSpent).FirstOrDefault();

            var tripActual = await service.GetShortestTripAsync();

            Assert.IsNotNull(tripActual);
            Assert.AreEqual(tripExpected.Title, tripActual.Title);
        }

        [Test]
        public async Task GetTripsCreatedByPeriod()
        {
            var startOfPeriod = DateTime.Now;
            var endOfPeriod = DateTime.Now.AddMilliseconds(4);
            var tripsCreatedExpected = ArrangeTests.ApplicationContext.Trips
                .Where(x => x.TripStart > startOfPeriod && x.TripEnd < endOfPeriod).Count();

            var tripscreatedActual =await service.GetTripsCreatedByPeriodAsync(startOfPeriod, endOfPeriod);

            Assert.IsNotNull(tripscreatedActual);
            Assert.AreEqual(tripsCreatedExpected, tripscreatedActual);
        }

        [Test]
        public async Task GetTripsByLowPriceTest()
        {
            var count = 3;
            var trips = ArrangeTests.ApplicationContext
                .Trips.OrderBy(x => x.Price.Value).Take(count);

            var tripsByLowPrice =await service.GetTripsByLowPriceAsync(count);

            Assert.IsNotEmpty(tripsByLowPrice);
            Assert.AreEqual(tripsByLowPrice.Count(), count);
        }
    }
}
