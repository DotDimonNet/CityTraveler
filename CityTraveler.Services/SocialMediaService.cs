using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly ILogger<SocialMediaService> _logger;
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public SocialMediaService(ApplicationContext context, IMapper mapper, ILogger<SocialMediaService> logger)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<EntertainmentReviewDTO> AddReviewEntertainment(Guid enterId, EntertainmentReviewDTO review)
        {
            if (!_dbContext.Entertaiments.Any(x => x.Id == enterId))
            {
                _logger.LogWarning("Entertainment not found");
                return null;
            }
            try
            {
                review.EntertainmentId = enterId;
                var model = _mapper.Map<EntertainmentReviewDTO, EntertainmentReviewModel>(review);
                //_dbContext.Ratings.Add(model.Rating);
                _dbContext.Reviews.Add(model);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to add review to entertainment {e.Message}");
                return null;
            }
            return review;
        }

        public async Task<TripReviewDTO> AddReviewTrip(Guid tripId, TripReviewDTO review)
        {
            if (!_dbContext.Trips.Any(x => x.Id == tripId))
            {
                _logger.LogWarning("Trip not found");
                return null;
            }
            try
            {
                review.TripId = tripId;
                var model = _mapper.Map<TripReviewDTO, TripReviewModel>(review);
                _dbContext.Reviews.Add(model);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to add review to trip {e.Message}");
                return null;
            }
            return review;
        }

        public IEnumerable<ReviewModel> GetObjectReviews(Guid objectId)
        {
            EntertaimentModel e = _dbContext.Entertaiments.FirstOrDefault(x=> x.Id == objectId);
            TripModel t = _dbContext.Trips.FirstOrDefault(x => x.Id == objectId);
            if (e != null)
            {
                return e.Reviews;
            }
            else if (t != null)
            {
                return t.Reviews;
            }
            else
            {
                _logger.LogWarning("Object not found");
                return null;
            }
        }

        public IEnumerable<ReviewModel> GetReviews(int skip = 0, int take = 10)
        {
            if (skip < 0 || take < 0)
            {
                _logger.LogWarning("Invalid arguments");
                return null;
            }
            return _dbContext.Reviews.Skip(skip).Take(take);
        }

        public IEnumerable<ReviewModel> GetUserReviews(Guid userId)
        {
            if (!_dbContext.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            return _dbContext.Reviews.Where(x => x.UserId == userId);
        }

        public async Task<bool> PostRating(RatingModel rating)
        {
            try
            {
                _dbContext.Ratings.Add(rating);
                return true;
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to post rating {e.Message}");
                return false;
            }

        }

        public async Task<bool> RemoveReview(Guid reviewId)
        {
            if (_dbContext.Reviews.Where(x => x.Id == reviewId).Count() == 0)
            {
                _logger.LogWarning("Review not found");
                return false;
            }
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
                _logger.LogWarning($"Failed to remove review {e.Message}");
                return false;
            }
        }
        public async Task<bool> AddComment(CommentDTO comment) 
        {
            try
            {
                var model = _mapper.Map<CommentDTO, CommentModel>(comment);
                model.Status = _dbContext.CommentStatuses.FirstOrDefault(x => x.Id == comment.Status);

                _dbContext.Comments.Add(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to add comment {e.Message}");
                return false;
            }
        }
        public async Task<bool> RemoveComment(Guid commentId)
        {
            if (!_dbContext.Comments.Any(x => x.Id == commentId))
            {
                return false;
                _logger.LogWarning("Comment not found");
            }

            try
            {
                CommentModel comment = await _dbContext.Comments.FirstOrDefaultAsync(x=> x.Id == commentId);
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to remove comment {e}");
                return false;
            }
        }
        public async Task<bool> AddImage(ReviewImageModel image) 
        {
            try
            {
                _dbContext.Images.Add(image);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to add image {e}");
                return false;
            }
        }
        public async Task<bool> RemoveImage(Guid reviewImageId) 
        {
            if (!_dbContext.Images.Any(x => x.Id == reviewImageId))
            {
                _logger.LogWarning("Image not found"); 
                return false;
            }
            try
            {
                ImageModel image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == reviewImageId);
                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to remove image {e}");
                return false;
            }
        }

        public IEnumerable<ReviewModel> GetReviewsByTitle(string title = "")
        {
            return _dbContext.Reviews.Where(x => x.Title.Contains(title));
        }

        public IEnumerable<ReviewModel> GetReviewsByAverageRating(double rating)
        {
            return _dbContext.Reviews.Where(x=>x.Rating.Value == rating);
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
            if (!_dbContext.Reviews.Any(x => x.Id == Id))
            {
                _logger.LogWarning("Review not found");
                return null;
            }
            return await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<bool> RemoveRating(Guid ratingId)
        {
            if (!_dbContext.Ratings.Any(x => x.Id == ratingId))
            {
                _logger.LogWarning("Rating not found");
                return false;
            }
             _dbContext.Ratings.Remove(await _dbContext.Ratings.FirstOrDefaultAsync(x => x.Id == ratingId));
            return true;
        }
        
    }
}
