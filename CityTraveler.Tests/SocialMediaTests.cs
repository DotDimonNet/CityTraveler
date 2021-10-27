using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace CityTraveler.Tests
{
    public class SocialMediaTests
    {
        [SetUp]
        public async Task Setup()
        {
            await ArrangeTests.SetupDbContext();
        }

        [Test]
        public async Task GetReviewByIdTest()
        {
            var reviewId = ArrangeTests.ApplicationContext.Reviews
                .FirstOrDefault().Id;
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);

            var review = await service.GetReviewGyId(reviewId);
            Assert.IsNotNull(review);
        }
        [Test]
        public void GetReviewsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var review = service.GetReviews(0,5);
            var initial = ArrangeTests.ApplicationContext.Reviews;
            Assert.IsNotNull(review);
            Assert.True(review.Count() == 5);
            for (int i = 0; i < 5; i++)
            {
                Assert.True(review.ElementAt(i) == initial.ElementAt(i));
            }
        }
        [Test]
        public async Task AddReviewEntertainmentTest()
        {
            var entertainmentId = ArrangeTests.ApplicationContext.Entertaiments
               .FirstOrDefault().Id;
            var user = ArrangeTests.ApplicationContext.Users
               .FirstOrDefault();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            EntertainmentReviewModel enR = new EntertainmentReviewModel { Rating = new RatingModel { Value = 5 }, User = user };
            var review = await service.AddReviewEntertainment(entertainmentId, enR);
            Assert.IsNotNull(review);
            Assert.True(ArrangeTests.ApplicationContext.Reviews.Contains(review));
        }
        [Test]
        public async Task AddReviewTripTest()
        {
            var triptId = ArrangeTests.ApplicationContext.Trips
               .FirstOrDefault().Id;
            var user = ArrangeTests.ApplicationContext.Users
               .FirstOrDefault();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            TripReviewModel tR = new TripReviewModel { Rating = new RatingModel { Value = 5 }, User = user };
            var review = await service.AddReviewTrip(triptId, tR);
            Assert.IsNotNull(review);
            Assert.True(ArrangeTests.ApplicationContext.Reviews.Contains(review));
        }
        [Test]
        public async Task RemoveReviewTest()
        {
            var review = await ArrangeTests.ApplicationContext.Reviews.FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var res = await service.RemoveReview(review.Id);
            Assert.True(res);
            Assert.True(!ArrangeTests.ApplicationContext.Reviews.Contains(review));
        }
        [Test]
        public void RemoveReviewThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>(async () => await service.RemoveReview(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("Review not found")); 
        }
        [Test]
        public void GetUserReviewsThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>( () =>  service.GetUserReviews(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }
        [Test]
        public async void GetUserReviewsTest()
        {
            var user = await ArrangeTests.ApplicationContext.Users.FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetUserReviews(user.Id);
            Assert.NotNull(reviews);
        }
        [Test]
        public async void GetObjectReviewsTest()
        {
            var trip = await ArrangeTests.ApplicationContext.Trips
              .FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetObjectReviews(trip.Id);
            
        }
        [Test]
        public void GetObjectReviewsThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>(() => service.GetObjectReviews(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("Object not found"));
        }
    }
}
