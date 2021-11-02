using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
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

            var review = await service.GetReviewById(reviewId);
            Assert.IsNotNull(review);
        }
        [Test]
        public void GetReviewsByIdThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.GetReviewById(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("Review not found"));
        }
        [Test]
        public void GetReviewsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var review = service.GetReviews(0, 5);
            var initial = ArrangeTests.ApplicationContext.Reviews;
            Assert.IsNotNull(review);
            Assert.True(review.Count() == 5);
            List<ReviewModel> l1 = review.ToList();
            for (int i = 0; i < 5; i++)
            {
                Assert.True(l1.ElementAt(i) == initial.ToList().ElementAt(i));
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
            EntertainmentReviewModel enR = new EntertainmentReviewModel
            {
                Rating = new RatingModel { Value = 5 },
                User = user
            };
            var review = await service.AddReviewEntertainment(entertainmentId, enR);
            Assert.IsNotNull(review);
            Assert.True(ArrangeTests.ApplicationContext.Reviews.Contains(review));
        }
        [Test]
        public void GetReviewsThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>(() => service.GetReviews(-1, 5));
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments"));
        }
        [Test]
        public void GetReviewsThrowsTest1()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>(() => service.GetReviews(20, 5));
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments"));
        }
        [Test]
        public void GetReviewsThrowsTest2()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>(() => service.GetReviews(1, -5));
            Assert.That(ex.Message, Is.EqualTo("Invalid arguments"));
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
        public void AddReviewEntertainmentThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.AddReviewEntertainment(
                Guid.NewGuid(), new EntertainmentReviewModel { Rating = new RatingModel { Value = 5 } }));
            Assert.That(ex.Message, Is.EqualTo("Entertainment not found"));
        }
        [Test]
        public void AddReviewTripThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.AddReviewTrip(Guid.NewGuid(),
                new TripReviewModel { Rating = new RatingModel { Value = 5 } }));
            Assert.That(ex.Message, Is.EqualTo("Trip not found"));
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
            var ex = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.RemoveReview(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("Review not found"));
        }
        [Test]
        public void GetUserReviewsThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>(() => service.GetUserReviews(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("User not found"));
        }
        [Test]
        public async Task GetUserReviewsTest()
        {
            var user = await ArrangeTests.ApplicationContext.Users.FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetUserReviews(user.Id);
            Assert.NotNull(reviews);
            foreach (ReviewModel review in reviews)
            {
                Assert.True(review.UserId == user.Id);
            }

        }
        [Test]
        public async Task GetObjectReviewsTest()
        {
            var trip = await ArrangeTests.ApplicationContext.Trips
              .FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetObjectReviews(trip.Id);
            Assert.NotNull(reviews);
        }
        [Test]
        public void GetObjectReviewsThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.Throws<SocialMediaServiceException>(() => service.GetObjectReviews(Guid.NewGuid()));
            Assert.That(ex.Message, Is.EqualTo("Object not found"));
        }
        [Test]
        public void PostRatingThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var result = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.PostRating(
                new RatingModel { Value = 3 }, Guid.NewGuid())); ;
            Assert.That(result.Message, Is.EqualTo("Review not found"));
        }
        [Test]
        public async Task PostRatingTest()
        {
            var firstReview = await ArrangeTests.ApplicationContext.Reviews
              .FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var review = await service.PostRating(new RatingModel { Value = 3 }, firstReview.Id);
            Assert.NotNull(review);
            Assert.True(ArrangeTests.ApplicationContext.Reviews.Contains(review));
        }
        [Test]
        public async Task AddCommentTest()
        {
            var firstReview = await ArrangeTests.ApplicationContext.Reviews
              .FirstOrDefaultAsync(x => x.Comments.Count() > 0);
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            CommentModel comment = new CommentModel { Status = CommentStatus.Liked };
            var review = await service.AddComment(comment, firstReview.Id);
            Assert.True(review);
            Assert.True(ArrangeTests.ApplicationContext.Comments.Contains(comment));
        }
        [Test]
        public void AddCommentThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var result = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.AddComment(
                new CommentModel { Status = CommentStatus.Liked }, Guid.NewGuid())); ;
            Assert.That(result.Message, Is.EqualTo("Review not found"));
        }

        [Test]
        public async Task RemoveCommentTest()
        {
            var firstReview = await ArrangeTests.ApplicationContext.Reviews
              .FirstOrDefaultAsync(x => x.Comments.Count() > 0);
            var comment = await ArrangeTests.ApplicationContext.Comments.FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var review = await service.RemoveComment(comment.Id, firstReview.Id);
            Assert.True(review);
            Assert.True(!ArrangeTests.ApplicationContext.Comments.Contains(comment));
        }
        [Test]
        public async Task RemoveCommentThrowsReviewTest()
        {
            var comment = await ArrangeTests.ApplicationContext.Comments.FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.RemoveComment(
                comment.Id, Guid.NewGuid())); ;
            Assert.That(ex.Message, Is.EqualTo("Review not found"));
        }
        [Test]
        public async Task RemoveCommentThrowsCommentTest()
        {
            var firstReview = await ArrangeTests.ApplicationContext.Reviews
             .FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.RemoveComment(
                Guid.NewGuid(), firstReview.Id)); ;
            Assert.That(ex.Message, Is.EqualTo("Comment not found"));
        }
        [Test]
        public void GetReviewsByTitleTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetReviewsByTitle("title");
            Assert.NotNull(reviews);
            foreach (ReviewModel review in reviews)
            {
                Assert.True(review.Title.Contains("title"));
            }
        }
        [Test]
        public void GetReviewsByDescriptionTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetReviewsByDescription("description");
            Assert.NotNull(reviews);
            foreach (ReviewModel review in reviews)
            {
                Assert.True(review.Description.Contains("description"));
            }
        }
        [Test]
        public async Task GetReviewsByCommentTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetReviewsByComment(await ArrangeTests.ApplicationContext.Comments.FirstOrDefaultAsync());
            Assert.NotNull(reviews);
            foreach (ReviewModel review in reviews)
            {
                Assert.True(review.Comments.Contains(await ArrangeTests.ApplicationContext.Comments.FirstOrDefaultAsync()));
            }
        }
        [Test]
        public void GetReviewsByAverageRaitingTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var reviews = service.GetReviewsByAverageRaiting(5);
            Assert.NotNull(reviews);
            foreach (ReviewModel review in reviews)
            {
                Assert.True(review.Rating.Value == 5);
            }
        }
        [Test]
        public async Task AddImageTest()
        {
            var firstReview = await ArrangeTests.ApplicationContext.Reviews
              .FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            ReviewImageModel image = new ReviewImageModel { Title = "title" };
            var review = await service.AddImage(image, firstReview.Id);
            Assert.True(review);
            Assert.True(ArrangeTests.ApplicationContext.Images.Contains(image));
        }
        [Test]
        public void AddImageThrowsTest()
        {
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var result = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.AddImage(
                new ReviewImageModel { Title = "title" }, Guid.NewGuid())); ;
            Assert.That(result.Message, Is.EqualTo("Review not found"));
        }

        [Test]
        public async Task RemoveImageTest()
        {
            var firstReview = await ArrangeTests.ApplicationContext.Reviews
              .FirstOrDefaultAsync(x => x.Images.Count() > 0);
            var image = await ArrangeTests.ApplicationContext.Images.FirstOrDefaultAsync(
                x => x.Id == firstReview.Images.ElementAt(0).Id);
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var review = await service.RemoveImage(image.Id, firstReview.Id);
            Assert.True(review);
            Assert.True(!ArrangeTests.ApplicationContext.Images.Contains(image));
        }
        [Test]
        public async Task RemoveImageThrowsReviewTest()
        {
            var image = await ArrangeTests.ApplicationContext.Images.FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var result = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.RemoveImage(
                image.Id, Guid.NewGuid())); ;
            Assert.That(result.Message, Is.EqualTo("Review not found"));
        }
        [Test]
        public async Task RemoveImageThrowsImageTest()
        {
            var firstReview = await ArrangeTests.ApplicationContext.Reviews
             .FirstOrDefaultAsync();
            var service = new SocialMediaService(ArrangeTests.ApplicationContext);
            var ex = Assert.ThrowsAsync<SocialMediaServiceException>(() => service.RemoveImage(Guid.NewGuid(),
                firstReview.Id)); ;
            Assert.That(ex.Message, Is.EqualTo("Image not found"));
        }
    }
}
