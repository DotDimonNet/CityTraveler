using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    interface IInfoService : IServiceMetadata
    {
        public Task<EntertaimentModel> GetEntertaimentByMaxChoiceInTrip(Guid userId = default);
        public TripModel GetTripByMaxChoiceOfUsers();
        public Task<ReviewModel> GetReviewByMaxComment(Guid userId = default);
        public Task<TripModel> GetMostlyUsedTemplate();
        public Task<TripModel> GetTripByMaxReview(Guid userId = default);
        public IEnumerable<TripModel> GetLastTripsByPeriod(DateTime srart, DateTime end);
        public TripModel GetTripByLowPrice();
        public int GetRegisteredUserByPeriod(DateTime start, DateTime end);
        public int GetUsersTripsDateRange(DateTime start, DateTime end);
        public TripModel GetLongestTrip();
        


    }
}
