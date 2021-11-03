using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ISocialMediaService : IServiceMetadata
    {
        Task<EntertainmentReviewDTO> AddReviewEntertainment(Guid enterId, EntertainmentReviewDTO rev);
        Task<TripReviewDTO> AddReviewTrip(Guid tripId, TripReviewDTO rev);
        Task<bool> RemoveReview(Guid reviewId);
        Task<bool> RemoveRating (Guid ratingId);
        Task<bool> UpdateReview(ReviewModel model);
        Task<bool> UpdateComment(CommentModel model);
        IEnumerable<ReviewDTO> GetReviews(int skip = 0, int take = 10);
        IEnumerable<ReviewDTO> GetUserReviews(Guid userId);
        IEnumerable<ReviewDTO> GetObjectReviews(Guid objectId);
        Task<bool> PostRating(RatingDTO rating);
        Task<bool> AddComment(CommentDTO comment);
        Task<bool> RemoveComment(Guid commentId);
        Task<bool> AddImage(ReviewImageDTO comment);
        Task<bool> RemoveImage(Guid reviewImageId);
        IEnumerable<ReviewDTO> GetReviewsByTitle(string title);
        IEnumerable<ReviewDTO> GetReviewsByDescription(string description);
        IEnumerable<ReviewDTO> GetReviewsByAverageRating(double raiting);
        Task<ReviewDTO> GetReviewByComment(Guid comment);
        Task<ReviewDTO> GetReviewById(Guid Id);

    }
}
