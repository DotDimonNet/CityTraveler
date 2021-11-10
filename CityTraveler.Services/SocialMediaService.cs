using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly ILogger<SocialMediaService> _logger;
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;

        public SocialMediaService(ApplicationContext context, IMapper mapper, ILogger<SocialMediaService> logger)
        {
            _dbContext = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EntertainmentReviewDTO> AddReviewEntertainment(Guid enterId, EntertainmentReviewDTO review)
        {
            try
            {
                if (!_dbContext.Entertaiments.Any(x => x.Id == enterId))
                {
                    _logger.LogError("Entertainment not found");
                    return null;
                }
                review.EntertainmentId = enterId;
                var model = _mapper.Map<EntertainmentReviewDTO, EntertainmentReviewModel>(review);
                _dbContext.Reviews.Add(model);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add review to entertainment {e.Message}");
                return null;
            }
            return review;
        }

        public async Task<TripReviewDTO> AddReviewTrip(Guid tripId, TripReviewDTO review)
        {
            try
            {
                if (!_dbContext.Trips.Any(x => x.Id == tripId))
                {
                    _logger.LogError("Trip not found");
                    return null;
                }
                review.TripId = tripId;
                var model = _mapper.Map<TripReviewDTO, TripReviewModel>(review);
                _dbContext.Reviews.Add(model);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add review to trip {e.Message}");
                return null;
            }
            return review;
        }

        public async Task<IEnumerable<ReviewDTO>> GetObjectReviews(Guid objectId)
        {
            try
            {
                if (!await _dbContext.Entertaiments.AnyAsync(x => x.Id == objectId)
                && !await _dbContext.Trips.AnyAsync(x => x.Id == objectId))
                {
                    _logger.LogError("Object not found");
                    return Enumerable.Empty<ReviewDTO>();
                }
                var entertainment = await _dbContext.Entertaiments.FirstOrDefaultAsync(x => x.Id == objectId);
                var trip = await _dbContext.Trips.FirstOrDefaultAsync(x => x.Id == objectId);
                return await _dbContext.Entertaiments.AnyAsync(x => x.Id == objectId) ?
                     entertainment.Reviews.Select(x => _mapper.Map<EntertainmentReviewModel, EntertainmentReviewDTO>(x)) :
                     trip.Reviews.Select(x => _mapper.Map<TripReviewModel, TripReviewDTO>(x));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get reviews {e.Message}");
                return Enumerable.Empty<ReviewDTO>();
            }
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviews(int skip = 0, int take = 10)
        {
            try
            {
                if (skip < 0 || take < 0)
                {
                    _logger.LogError("Invalid arguments");
                    return Enumerable.Empty<ReviewDTO>();
                }
                var reviewModels = _dbContext.Reviews.Skip(skip).Take(take);
                var reviews = new List<ReviewDTO>();
                return await Task.Run(() => reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get reviews {e.Message}");
                return Enumerable.Empty<ReviewDTO>();
            }
        }

        public async Task<IEnumerable<ReviewDTO>> GetUserReviews(Guid userId)
        {
            try
            {
                if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
                {
                    _logger.LogError("User not found");
                    return Enumerable.Empty<ReviewDTO>();
                }
                var reviewModels = _dbContext.Reviews.Where(x => x.UserId == userId);
                return reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get reviews {e.Message}");
                return Enumerable.Empty<ReviewDTO>();
            }
        }

        public async Task<bool> PostRating(RatingDTO rating)
        {
            try
            {
                if (!await _dbContext.Reviews.AnyAsync(x => x.Id == rating.ReviewId))
                {
                    _logger.LogError($"Review not found");
                    return false;
                }
                var model = _mapper.Map<RatingDTO, RatingModel>(rating);
                _dbContext.Ratings.Add(model);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to post rating {e.Message}");
                return false;
            }

        }

        public async Task<bool> RemoveReview(Guid reviewId)
        {
            try
            {
                if (!_dbContext.Reviews.Any(x => x.Id == reviewId))
                {
                    _logger.LogError("Review not found");
                    return false;
                }
                var review = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
                var raiting = await _dbContext.Ratings.FirstOrDefaultAsync(x => x.ReviewId == review.Id);
                if(raiting!=null)
                    _dbContext.Ratings.Remove(raiting);
                _dbContext.Images.RemoveRange(review.Images);
                _dbContext.Reviews.Remove(review);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to remove review {e.Message}");
                return false;
            }
        }

        public async Task<bool> AddComment(CommentDTO comment)
        {
            try
            {
                if (!_dbContext.Reviews.Any(x => x.Id == comment.ReviewId))
                {
                    _logger.LogError("Review not found");
                    return false;
                }
                if (comment.Status > 3 || comment.Status < 1)
                {
                    _logger.LogError("Comment status incorrect");
                    return false;
                }
                var model = _mapper.Map<CommentDTO, CommentModel>(comment);
                model.Status = (CommentStatus)comment.Status;
                _dbContext.Comments.Add(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add comment {e.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveComment(Guid commentId)
        {
            try
            {
                if (!_dbContext.Comments.Any(x => x.Id == commentId))
                {
                    _logger.LogError("Review not found");
                    return false;
                }
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to remove comment {e.Message}");
                return false;
            }
        }

        public async Task<bool> AddImage(ReviewImageDTO image)
        {
            try
            {
                if (!_dbContext.Reviews.Any(x => x.Id == image.ReviewId))
                {
                    _logger.LogError("Review not found");
                    return false;
                }
                var model = _mapper.Map<ReviewImageDTO, ReviewImageModel>(image);
                _dbContext.Images.Add(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add image {e.Message}");
                return false;
            }
        }

        public async Task<bool> RemoveImage(Guid reviewImageId)
        {
            try
            {
                if (!_dbContext.Images.Any(x => x.Id == reviewImageId))
                {
                    _logger.LogError("Image not found");
                    return false;
                }
                var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.Id == reviewImageId);
                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to remove image {e.Message}");
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
                _logger.LogError($"Failed to get review by title {e.Message}");
                return Enumerable.Empty<ReviewDTO>();
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
                _logger.LogError($"Failed to get review by average raiting {e.Message}");
                return Enumerable.Empty<ReviewDTO>();
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
                _logger.LogError($"Failed to get review by comment {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ReviewDTO>> GetReviewsByDescription(string description = "")
        {
            try
            {
                var reviewModels = _dbContext.Reviews.Where(x => x.Description.Contains(description ?? ""));
                return await Task.Run(() => reviewModels.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get review by description {e.Message}");
                return Enumerable.Empty<ReviewDTO>();
            }
        }

        public async Task<ReviewDTO> GetReviewById(Guid Id)
        {
            try
            {
                if (!_dbContext.Reviews.Any(x => x.Id == Id))
                {
                    _logger.LogError("Review not found");
                    return null;
                }
                return _mapper.Map<ReviewModel, ReviewDTO>(await _dbContext.Reviews.FirstOrDefaultAsync(x => x.Id == Id));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get review by id {e.Message}");
                return null;
            }
        }

        public async Task<bool> RemoveRating(Guid ratingId)
        {
            try
            {
                if (!await _dbContext.Ratings.AnyAsync(x => x.Id == ratingId))
                {
                    _logger.LogError("Rating not found");
                    return false;
                }
                _dbContext.Ratings.Remove(await _dbContext.Ratings.FirstOrDefaultAsync(x => x.Id == ratingId));
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to remove raiting {e.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateReview(Guid Id, ReviewDTO model)
        {
            try
            {
                if (!await _dbContext.Reviews.AnyAsync(x => x.Id == Id))
                {
                    _logger.LogError("Review not found");
                    return false;
                }
                _dbContext.Reviews.Update(_mapper.Map<ReviewDTO, ReviewModel>(model));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update review {e.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateComment(Guid Id, CommentDTO model)
        {
            try
            {
                var isCommentExists = await _dbContext.Comments.AnyAsync(x => x.Id == Id);
                if (!isCommentExists)
                {
                    _logger.LogError("Comment not found");
                    return false;
                }
                _dbContext.Comments.Update(_mapper.Map<CommentDTO, CommentModel>(model));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update comment {e.Message}");
                return false;
            }
        }
    }
}
