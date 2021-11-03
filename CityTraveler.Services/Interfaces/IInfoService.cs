using CityTraveler.Domain.DTO;
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
        public Task<EntertainmentShowDTO> GetMostPopularEntertaimentInTrips(Guid userId = default);
        public Task<TripDTO> GetTripByMaxChoiceOfUsers();
        public Task<ReviewDTO> GetReviewByMaxComments(Guid userId = default);
        public IEnumerable<TripDTO> GetMostlyUsedTemplates(int count);
        public Task<TripDTO> GetTripByMaxReview(Guid userId = default);
        public IEnumerable<TripDTO> GetLastTripsByPeriod(DateTime srart, DateTime end);
        public IEnumerable<TripDTO> GetTripsByLowPrice(int count);
        public Task<int> GetRegisteredUsersByPeriod(DateTime start, DateTime end);
        public int GetUsersCountTripsDateRange(DateTime start, DateTime end);
        public Task<TripDTO> GetLongestTrip();
        public Task<TripDTO> GetShortestTrip();
        public int GetTripsCreatedByPeriod(DateTime start, DateTime end);




    }
}
