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
        public Task<ReviewPreviewDTO> GetLastReview();
        public Task<TripPrewievDTO> GetLastTrip();
        public Task<TripPrewievDTO> GetUserLastTrip(Guid userId, bool passed = false);
        public Task<IEnumerable<EntertainmentPreviewDTO>> GetVisitEntertaiment(Guid userId, bool withoutReview = false);
        public Task<ReviewPreviewDTO> GetUserLastReview(Guid userId);
        public Task<IEnumerable<CommentDTO>> GetUserComments(Guid userId);
        //public Task<Friend> GetLastAddedFriend(Guid userId);

    }
}
