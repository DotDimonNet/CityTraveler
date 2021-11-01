using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IInfoService : IServiceMetadata
    {
        public Task<EntertaimentModel> GetMostPopularEntertaimentInTrips(Guid userId = default);
        public Task<TripModel> GetTripByMaxChoiceOfUsers();
        public Task<ReviewModel> GetReviewByMaxComments(Guid userId = default);
        public IEnumerable<TripModel> GetMostlyUsedTemplates(int count);
        public Task<TripModel> GetTripByMaxReview(Guid userId = default);
        public IEnumerable<TripModel> GetLastTripsByPeriod(DateTime srart, DateTime end);
        public IEnumerable<TripModel> GetTripsByLowPrice(int count);
        public Task<int> GetRegisteredUsersByPeriod(DateTime start, DateTime end);
        public int GetUsersCountTripsDateRange(DateTime start, DateTime end);
        public Task<TripModel> GetLongestTrip();
        public Task<TripModel> GetShortestTrip();
        public int GetTripsCreatedByPeriod(DateTime start, DateTime end);




    }
}
