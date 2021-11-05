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

        public async Task<IEnumerable<ReviewDTO>> GetObjectReviews(Guid objectId)
        {
            if (!_dbContext.Entertaiments.Any(x => x.Id == objectId) && !_dbContext.Trips.Any(x => x.Id == objectId)) 
            {
                _logger.LogWarning("Object not found");
                return null;
            }
            try
            {
                var entertainment = _dbContext.Entertaiments.FirstOrDefault(x => x.Id == objectId);
                var trip = _dbContext.Trips.FirstOrDefault(x => x.Id == objectId);
                return await _dbContext.Entertaiments.AnyAsync(x => x.Id == objectId) ?
                     entertainment.Reviews.Select(x => _mapper.Map<EntertainmentReviewModel, EntertainmentReviewDTO>(x)) :
                     trip.Reviews.Select(x => _mapper.Map<TripReviewModel, TripReviewDTO>(x));
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to get reviews {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviews(int skip = 0, int take = 10)
        {
            if (skip < 0 || take < 0)
            {
                _logger.LogWarning("Invalid arguments");
                return null;
            }
            try { 
            var reviewModels= _dbContext.Reviews.Skip(skip).Take(take);
            var reviews = new List<ReviewDTO>();
            return await Task.Run(() => reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x)));
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to get reviews {e.Message}");
                return null;
            }
}

        public async Task<IEnumerable<ReviewDTO>> GetUserReviews(Guid userId)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                var reviewModels = _dbContext.Reviews.Where(x => x.UserId == userId);
                return reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x));
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to get reviews {e.Message}");
                return null;
            }
        }

        public async Task<bool> PostRating(RatingDTO rating)
        {
            if (!await _dbContext.Reviews.AnyAsync(x => x.Id == rating.ReviewId))
            {
                _logger.LogWarning($"Review not found");
                return false;
            }
            try
            {
                var model = _mapper.Map<RatingDTO, RatingModel>(rating);
                _dbContext.Ratings.Add(model);
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
            if (!_dbContext.Reviews.Where(x => x.Id == reviewId).Any())
            {
                _logger.LogWarning("Review not found");
                return false;
            }
            try
            {
                var review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
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
            if (!_dbContext.Reviews.Any(x => x.Id == comment.ReviewId)) 
            {
                _logger.LogWarning("Review not found");
                return false;
            }
            if (comment.Status > 3 || comment.Status < 1)
            {
                _logger.LogWarning("Comment status incorrect");
                return false;
            }
            try
            {
                var model = _mapper.Map<CommentDTO, CommentModel>(comment);
                if (comment.Status > 3 || comment.Status < 1)
                {
                    _logger.LogWarning("Comment status incorrect");
                    return false;
                }
                model.Status = (CommentStatus)comment.Status;
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
                _logger.LogWarning("Review not found");
                return false;
            }

            try
            {
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(x=> x.Id == commentId);
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to remove comment {e.Message}");
                return false;
            }
        }
        public async Task<bool> AddImage(ReviewImageDTO image) 
        {
            if (!_dbContext.Reviews.Any(x => x.Id == image.ReviewId))
            {
                _logger.LogWarning("Review not found");
                return false;
            }
            try
            {
                var model = _mapper.Map<ReviewImageDTO, ReviewImageModel>(image);
                _dbContext.Images.Add(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to add image {e.Message}");
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
                var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == reviewImageId);
                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to remove image {e.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByTitle(string title = "")
        {
            try
            {
                var reviewModels = _dbContext.Reviews.Where(x => x.Title.Contains(title));
                return await Task.Run(() => reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x)));
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to get review by title {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByAverageRating(double rating)
        {
            try
            {
                var reviewModels = _dbContext.Reviews.Where(x => x.Rating.Value == rating);
                return await Task.Run(() => reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x)));
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to get review by average raiting {e.Message}");
                return null;
            }
        }

        public async Task<ReviewDTO> GetReviewByComment(Guid comment)
        {
            try
            {
                var commentModel = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == comment);
                return _mapper.Map<ReviewModel, ReviewDTO>(commentModel.Review);
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to get review by comment {e.Message}");
                return null;
            }
        }
        public Task<IEnumerable<ReviewDTO>> GetReviewsByDescription(string description = "")
        {
            try
            {
                IEnumerable<ReviewModel> reviewModels = _dbContext.Reviews.Where(x => x.Description.Contains(description ?? ""));
                return Task.Run(() => reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x)));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to get review by description {e.Message}");
                return null;
            }
        }

        public async Task<ReviewDTO> GetReviewById(Guid Id)
        {
            if (!_dbContext.Reviews.Any(x => x.Id == Id))
            {
                _logger.LogWarning("Review not found");
                return null;
            }
            try
            {
                return _mapper.Map<ReviewModel, ReviewDTO>(await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == Id));
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to get review by id {e.Message}");
                return null;
            }
        }

        public async Task<bool> RemoveRating(Guid ratingId)
        {
            if (!await _dbContext.Ratings.AnyAsync(x => x.Id == ratingId))
            {
                _logger.LogWarning("Rating not found");
                return false;
            }
            try
            {
                _dbContext.Ratings.Remove(await _dbContext.Ratings.FirstOrDefaultAsync(x => x.Id == ratingId));
                return true;
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to remove raiting {e.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateReview(Guid Id, ReviewDTO model)
        {
            if (! await _dbContext.Reviews.AnyAsync(x => x.Id == Id))
            {
                _logger.LogWarning("Review not found");
                return false;
            }
            try
            {
                _dbContext.Reviews.Update(_mapper.Map<ReviewDTO, ReviewModel>(model));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to update review {e.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateComment(Guid Id, CommentDTO model)
        {
            if (! await _dbContext.Comments.AnyAsync(x => x.Id == Id))
            {
                _logger.LogWarning("Comment not found");
                return false;
            }
            try
            {
                _dbContext.Comments.Update(_mapper.Map<CommentDTO, CommentModel>(model));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to update comment {e.Message}");
                return false;
            }
        }
    }
}
