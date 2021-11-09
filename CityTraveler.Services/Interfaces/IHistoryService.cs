using CityTraveler.Domain.DTO;
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
    public interface IHistoryService
    {
        public Task<CommentDTO> GetUserLastComment(Guid userId);
        public Task<CommentDTO> GetLastComment();
        public Task<ReviewDTO> GetLastReview();
        public Task<TripDTO> GetLastTrip();
        public Task<TripDTO> GetUserLastTrip(Guid userId, bool passed = false);
        public Task<IEnumerable<EntertaimentModel>> GetVisitEntertaiment(Guid userId, bool withoutReview = false);
        public Task<ReviewDTO> GetUserLastReview(Guid userId);
        public Task<IEnumerable<CommentDTO>> GetUserComments(Guid userId);
        //public Task<Friend> GetLastAddedFriend(Guid userId);

    }
}
