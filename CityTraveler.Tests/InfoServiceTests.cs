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
                 .OrderBy(x => x.Trips.Count)
                 .FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);

            var entertaiment = await service.GetMostPopularEntertaimentInTrips(userModel.Id);

            Assert.IsNotNull(entertaiment);
            Assert.AreEqual(entertaiment.Id, mostPopularUserEntertaimentInTrips.Id);
            Assert.AreEqual(entertaiment.Title, mostPopularUserEntertaimentInTrips.Title);
        }

        /*[Test]
        public async Task GetMostPopularEntertaimentTripsTests()
        {
            var mostPopularEntertainment = ArrangeTests.ApplicationContext.Entertaiments
                .OrderBy(x => x.Trips.Count).FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var entertaiment = await service.GetMostPopularEntertaimentInTrips();

            Assert.IsNotNull(entertaiment);
            Assert.AreEqual(entertaiment.Id, mostPopularEntertainment.Id);
        }*/

        [Test]
        public async Task GetTripByMaxChoiceOfUsersTest()
        {
            var trip = ArrangeTests.ApplicationContext.Trips
                .OrderBy(x => x.Users.Count).FirstOrDefault();
            var service = new InfoService(ArrangeTests.ApplicationContext);
            var tripFromMethod = await service.GetTripByMaxChoiceOfUsers();
           
            Assert.AreEqual(tripFromMethod.Id, trip.Id);
                

        }
        
        /*[Test]
        public async Task GetUserReviewByMaxCommentTest()
        {
            var user = ArrangeTests.ApplicationContext.Users
                .LastOrDefault(x => x.Reviews.Count > 0);

            var userReview = ArrangeTests.ApplicationContext.Users
                .FirstOrDefault(x => x.UserId == user.Id).Reviews
                .OrderBy(x => x.Comments.Count)
                .FirstOrDefault();
                
            var serviceInfoService = new InfoService(ArrangeTests.ApplicationContext);
            var review = await serviceInfoService.GetReviewByMaxComment(user.Id);
            
            Assert.AreEqual(review.Id, userReview.Id);
                          
        }

        [Test]
        public async Task GetReviewByMaxCommentTest()
        {
            var reviewByMaxComment = ArrangeTests.ApplicationContext
                .Reviews
                .OrderBy(x => x.Comments.Count)
                .FirstOrDefault();
            var serviceInfoservice = new InfoService(ArrangeTests.ApplicationContext);
            var review = await serviceInfoservice.GetReviewByMaxComment();

            Assert.AreEqual(reviewByMaxComment.Id, review.Id);
        }*/



    }

}
