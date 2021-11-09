using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ISocialMediaService
    {
        Task<ReviewDTO> AddReviewEntertainment(Guid enterId, ReviewDTO rev);
        Task<ReviewDTO> AddReviewTrip(Guid tripId, ReviewDTO rev);
        Task<bool> RemoveReview(Guid reviewId);
        Task<bool> RemoveRating (Guid ratingId);
        Task<bool> UpdateReview(Guid Id, ReviewDTO model);
        Task<bool> UpdateComment(Guid Id, CommentDTO model);
        Task<IEnumerable<ReviewDTO>> GetReviews(int skip = 0, int take = 10);
        Task<IEnumerable<ReviewDTO>> GetUserReviews(Guid userId);
        Task<IEnumerable<ReviewDTO>> GetObjectReviews(Guid objectId);
        Task<bool> PostRating(RatingDTO rating);
        Task<bool> AddComment(CommentDTO comment);
        Task<bool> RemoveComment(Guid commentId);
        Task<bool> AddImage(ReviewImageDTO comment);
        Task<bool> RemoveImage(Guid reviewImageId);
        Task<IEnumerable<ReviewDTO>> GetReviewsByTitle(string title);
        Task<IEnumerable<ReviewDTO>> GetReviewsByDescription(string description);
        Task<IEnumerable<ReviewDTO>> GetReviewsByAverageRating(double raiting);
        Task<ReviewDTO> GetReviewByComment(Guid comment);
        Task<ReviewDTO> GetReviewById(Guid Id);

    }
}
