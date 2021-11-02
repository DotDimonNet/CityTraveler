﻿using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ISocialMediaService : IServiceMetadata
    {
        Task<EntertainmentReviewModel> AddReviewEntertainment(Guid enterId, EntertainmentReviewModel rev);
        Task<TripReviewModel> AddReviewTrip(Guid tripId, TripReviewModel rev);
        Task<bool> RemoveReview(Guid reviewId);
        Task<bool> RemoveRating (Guid ratingId);
        IEnumerable<ReviewModel> GetReviews(int skip = 0, int take = 10);
        IEnumerable<ReviewModel> GetUserReviews(Guid userId);
        IEnumerable<ReviewModel> GetObjectReviews(Guid objectId);
        Task<ReviewModel> PostRating(RatingModel rating, Guid reviewId);
        Task<bool> AddComment(CommentModel comment, Guid reviewId);
        Task<bool> RemoveComment(Guid commentId);
        Task<bool> AddImage(ReviewImageModel comment, Guid reviewId);
        Task<bool> RemoveImage(Guid reviewImageId);
        IEnumerable<ReviewModel> GetReviewsByTitle(string title);
        IEnumerable<ReviewModel> GetReviewsByDescription(string description);
        IEnumerable<ReviewModel> GetReviewsByAverageRating(double raiting);
        IEnumerable<ReviewModel> GetReviewsByComment(CommentModel comment);
        Task<ReviewModel> GetReviewById(Guid Id);

    }
}
