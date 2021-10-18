using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        /*private readonly IServiceContext _serviceContext;
        private readonly IDbContext _dbContext;
        public SocialMediaService(IServiceContext serviceContext, IDbContext dbContext)
        {
            _dbContext = dbContext;
            _serviceContext = serviceContext;
        }

        public async Task<IReview> AddReview(IReview rev)
        {
            _dbContext.Reviews.Collection.Add(rev);

            var query = $"";

            return await _dbContext.Reviews.RequestManager.ExecuteScalarAsync(query, null, false);
        }

        public async Task<IReview> AddReviewTrip(Guid tripId, IReview rev)
        {
            _dbContext.Reviews.Collection.Add(rev);

            var query = $"";

            return await _dbContext.Reviews.RequestManager.ExecuteScalarAsync(query, null, false);
        }

        public IEnumerable<IReview> GetObjectReviews(Guid objectId, PlaceType type)
        {
            switch (type)
            {
                case PlaceType.Event:
                    return _dbContext.Events.Collection.FirstOrDefault(x => x.Id == objectId).Reviews;
                case PlaceType.Institution:
                    return _dbContext.Institutions.Collection.FirstOrDefault(x => x.Id == objectId).Reviews;
                case PlaceType.Landscape:
                    return _dbContext.Landskapes.Collection.FirstOrDefault(x => x.Id == objectId).Reviews;
                default:
                    return Enumerable.Empty<IReview>();
            }
        }

        public IEnumerable<IReview> GetReviews(int skip = 0, int take = 10)
        {
            return _dbContext.Reviews.Collection.Skip(skip).Take(take);
        }

        public IEnumerable<IReview> GetTripReviews(Guid tripId)
        {
            return _dbContext.Trips.Collection.FirstOrDefault(x => x.Id == tripId).Reviews;
        }

        public IEnumerable<IReview> GetUserReviews(Guid userId)
        {
            return _dbContext.Reviews.Collection.Where(x => x.OwnerID == userId);
        }

        public async Task<IReview> PostRating(IRating rating, Guid reviewId)
        {
            _dbContext.Ratings.Collection.Add(rating);

            var review = _dbContext.Reviews.Collection.FirstOrDefault(x => x.Id == reviewId);
            review.Rating = rating;
            var query = $"";

            return await _dbContext.Reviews.RequestManager.ExecuteScalarAsync(query, null, false);
        }

        public async Task<bool> RemoveReview(Guid reviewId)
        {
            _dbContext.Reviews.Collection.RemoveAll(x => x.Id == reviewId);
            var query = $"";

            var affectedReviews = await _dbContext.Reviews.RequestManager.SendRequestAsync(query, null, false);
            return affectedReviews > 0;
        }*/
    }
}
