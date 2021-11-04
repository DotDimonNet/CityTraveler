using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Filters;
using CityTraveler.Domain.Filters.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IHistoryService : IServiceMetadata
    {
        public Task<CommentModel> GetUserLastComment(Guid userId);
        public Task<CommentModel> GetLastComment();
        public Task<ReviewModel> GetLastReview();
        public Task<TripModel> GetLastTrip();
        public Task<TripModel> GetUserLastTrip(Guid userId, bool passed = false);
        public Task<IEnumerable<EntertaimentModel>> GetVisitEntertaiment(Guid userId, bool withoutReview = false);
        public Task<ReviewModel> GetUserLastReview(Guid userId);
        public Task<IEnumerable<CommentModel>> GetUserComments(Guid userId);
        //public Task<Friend> GetLastAddedFriend(Guid userId);

    }
}
