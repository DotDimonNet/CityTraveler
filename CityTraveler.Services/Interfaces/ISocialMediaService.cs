using CityTraveler.Infrastructure.Enums;
using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ISocialMediaService 
    {
        Task<IReview> AddReview(IReview rev);
        Task<IReview> AddReviewTrip(Guid tripId, IReview rev);
        Task<bool> RemoveReview(Guid reviewId);
        IEnumerable<IReview> GetReviews(int skip = 0, int take = 10);
        IEnumerable<IReview> GetUserReviews(Guid userId);
        IEnumerable<IReview> GetTripReviews(Guid tripId);
        IEnumerable<IReview> GetObjectReviews(Guid objectId, PlaceType type);
        Task<IReview> PostRating(IRating rating, Guid reviewId);
    }
}
