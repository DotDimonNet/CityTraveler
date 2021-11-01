using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly ApplicationContext _dbContext;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public SocialMediaService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EntertainmentReviewModel> AddReviewEntertainment(Guid enterId, EntertainmentReviewModel rev)
        {
            if (_dbContext.Entertaiments.Where(x => x.Id == enterId).Count() == 0)
                throw new SocialMediaServiceException("Entertainment not found");
            try
            {
                rev.EntertaimentId = enterId;
                _dbContext.Reviews.Add(rev);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e) 
            {
                 throw new SocialMediaServiceException("Failed to add review to entertainment");
                //return rev;
            }
            return rev;
        }

        public async Task<TripReviewModel> AddReviewTrip(Guid tripId, TripReviewModel rev)
        {
            if (_dbContext.Trips.Where(x => x.Id == tripId).Count() == 0)
                throw new SocialMediaServiceException("Trip not found");
            try
            {
                rev.TripId = tripId;
                _dbContext.Reviews.Add(rev);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new SocialMediaServiceException("Failed to add review to trip");
                //return rev;
            }
            return rev;
        }

        public IEnumerable<ReviewModel> GetObjectReviews(Guid objectId)
        {
            EntertaimentModel e = _dbContext.Entertaiments.FirstOrDefault(x=> x.Id == objectId);
            TripModel t = _dbContext.Trips.FirstOrDefault(x => x.Id == objectId);
            if (e != null)
                return e.Reviews;
            else if (t != null)
                return t.Reviews;
            else
                throw new SocialMediaServiceException("Object not found");
        }

        public IEnumerable<ReviewModel> GetReviews(int skip = 0, int take = 10)
        {
            if (skip < 0|| take<0|| skip>take)
                throw new SocialMediaServiceException("Invalid arguments");
            return _dbContext.Reviews.Skip(skip).Take(take);
        }

        public IEnumerable<ReviewModel> GetUserReviews(Guid userId)
        {
            if (_dbContext.Users.Where(x => x.Id == userId).Count() == 0)
                throw new SocialMediaServiceException("User not found");
            return _dbContext.Reviews.Where(x => x.UserId == userId);
        }

        public async Task<ReviewModel> PostRating(RatingModel rating, Guid reviewId)
        {
            if (_dbContext.Reviews.Where(x => x.Id == reviewId).Count() == 0)
                throw new SocialMediaServiceException("Review not found");
            try
            {
                ReviewModel re = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
                _dbContext.Ratings.Add(rating);
                return re;
            }
            catch (Exception e) 
            {
                throw new SocialMediaServiceException("Failed to post raiting");
                //return null;
            }

        }

        public async Task<bool> RemoveReview(Guid reviewId)
        {
            if (_dbContext.Reviews.Where(x => x.Id == reviewId).Count() == 0)
                throw new SocialMediaServiceException("Review not found");
            try
            {
                ReviewModel review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
                var raiting = _dbContext.Ratings.FirstOrDefault(x => x.ReviewId == review.Id);
                var removeRaiting = _dbContext.Ratings.Remove(raiting);
                _dbContext.SaveChanges();
                for (int i = 0; i < review.Images.Count; i++)
                    _dbContext.Images.Remove(review.Images.ElementAt(i));
                _dbContext.SaveChanges();
                _dbContext.Reviews.Remove(review);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new SocialMediaServiceException("Failed to remove review");
                //return false;
            }
        }
        public async Task<bool> AddComment(CommentModel comment, Guid reviewId) 
        {
            if (_dbContext.Reviews.Where(x => x.Id == reviewId).Count() == 0)
                throw new SocialMediaServiceException("Review not found");
            try
            {
                comment.ReviewId = reviewId;
                _dbContext.Comments.Add(comment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e) 
            {
                throw new SocialMediaServiceException("Failed to add comment");
                //return false;
            }
        }
        public async Task<bool> RemoveComment(Guid commentId, Guid reviewId)
        {
            if (_dbContext.Reviews.Where(x => x.Id == reviewId).Count() == 0)
                throw new SocialMediaServiceException("Review not found");
            if (_dbContext.Comments.Where(x => x.Id == commentId).Count() == 0)
                throw new SocialMediaServiceException("Comment not found");
            if (_dbContext.Comments.Where(x => x.ReviewId == reviewId && x.Id == commentId).Count()==0)
                throw new SocialMediaServiceException("No relation between given comment and review");

            try
            {
                CommentModel comment = await _dbContext.Comments.FirstOrDefaultAsync(x=>x.ReviewId == reviewId && x.Id == commentId);
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new SocialMediaServiceException("Failed to remove comment");
                //return false;
            }
        }
        public async Task<bool> AddImage(ReviewImageModel image, Guid reviewId) 
        {
            if (_dbContext.Reviews.Where(x => x.Id == reviewId).Count() == 0)
                throw new SocialMediaServiceException("Review not found");
            try
            {
                image.ReviewId = reviewId;
                _dbContext.Images.Add(image);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new SocialMediaServiceException("Failed to add image");
                //return false;
            }
        }
        public async Task<bool> RemoveImage(Guid reviewImageId, Guid reviewId) 
        {
            if (_dbContext.Reviews.Where(x => x.Id == reviewId).Count() == 0)
                throw new SocialMediaServiceException("Review not found");
            if (_dbContext.Images.Where(x => x.Id == reviewImageId).Count() == 0)
                throw new SocialMediaServiceException("Image not found");
            try
            {
                ReviewImageModel image = (ReviewImageModel) await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == reviewImageId);
                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new SocialMediaServiceException("Failed to remove image");
                //return false;
            }
        }

        public IEnumerable<ReviewModel> GetReviewsByTitle(string title)
        {
            return _dbContext.Reviews.Where(x => x.Title.Contains(title ?? ""));
        }

        public IEnumerable<ReviewModel> GetReviewsByAverageRaiting(double raiting)
        {
            return _dbContext.Reviews.Where(x=>x.Rating.Value == raiting);
        }

        public IEnumerable<ReviewModel> GetReviewsByComment(CommentModel comment)
        {
            return _dbContext.Reviews.Where(x=>x.Comments.Contains(comment));
        }

        public IEnumerable<ReviewModel> GetReviewsByDescription(string description)
        {
            return _dbContext.Reviews.Where(x => x.Description.Contains(description ?? ""));
        }

        public async Task<ReviewModel> GetReviewById(Guid Id)
        {
            if (_dbContext.Reviews.Where(x => x.Id == Id).Count() == 0)
                throw new SocialMediaServiceException("Review not found");
            return await _dbContext.Reviews.FirstOrDefaultAsync(x=>x.Id == Id);
        }

        public Task<bool> RemoveRaiting(Guid ratingId)
        {
            throw new NotImplementedException();
        }
    }
}
