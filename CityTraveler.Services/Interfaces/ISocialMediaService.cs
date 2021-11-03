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
        IEnumerable<ReviewModel> GetReviews(int skip = 0, int take = 10);
        IEnumerable<ReviewModel> GetUserReviews(Guid userId);
        IEnumerable<ReviewModel> GetObjectReviews(Guid objectId);
        Task<bool> PostRating(RatingModel rating);
        Task<bool> AddComment(CommentDTO comment);
        Task<bool> RemoveComment(Guid commentId);
        Task<bool> AddImage(ReviewImageModel comment);
        Task<bool> RemoveImage(Guid reviewImageId);
        IEnumerable<ReviewModel> GetReviewsByTitle(string title);
        IEnumerable<ReviewModel> GetReviewsByDescription(string description);
        IEnumerable<ReviewModel> GetReviewsByAverageRating(double raiting);
        IEnumerable<ReviewModel> GetReviewsByComment(CommentModel comment);
        Task<ReviewModel> GetReviewById(Guid Id);

    }
}
