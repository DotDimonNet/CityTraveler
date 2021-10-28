﻿using CityTraveler.Domain.Entities;
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
            return _dbContext.Reviews.Skip(skip).Take(take);
        }

        public IEnumerable<ReviewModel> GetUserReviews(Guid userId)
        {
            return _dbContext.Reviews.Where(x=>x.UserId == userId);
        }

        public async Task<ReviewModel> PostRating(RatingModel rating, Guid reviewId)
        {
            try
            {
                ReviewModel re = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
                _dbContext.Reviews.Remove(re);
               // re.Rating = rating;
                re.RatingId = rating.Id;
                _dbContext.Reviews.Add(re);
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
            try
            {
                ReviewModel re =await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
                if(re==null)
                    throw new SocialMediaServiceException("Review not found");
                _dbContext.Reviews.Remove(re);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new SocialMediaServiceException("Failed to remove review");
                //return false;
            }
        }
        public async Task<bool> AddComment(CommentModel comment, Guid reviewId) 
        {
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
            try
            {
                CommentModel comment = await _dbContext.Comments.FirstOrDefaultAsync(x=>x.ReviewId == reviewId && x.Id == commentId);
                if(comment == null)
                    throw new SocialMediaServiceException("Comment not found");
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
            try
            {
                ReviewImageModel image = (ReviewImageModel) await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == reviewImageId);
                if(image == null)
                    throw new SocialMediaServiceException("Image not found");
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

            //return _dbContext.Reviews.Where(x=>x.Rating == raiting);
            return null;
        }

        public IEnumerable<ReviewModel> GetReviewsByComment(CommentModel comment)
        {
            return _dbContext.Reviews.Where(x=>x.Comments.Contains(comment));
        }

        public IEnumerable<ReviewModel> GetReviewsByDescription(string description)
        {
            return _dbContext.Reviews.Where(x => x.Description.Contains(description ?? ""));
        }
    }
}
