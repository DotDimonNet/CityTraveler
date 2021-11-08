using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IInfoService 
    {
        public Task<EntertainmentShowDTO> GetMostPopularEntertaimentInTripsAsync(Guid userId = default);
        public Task<TripDTO> GetMostPopularTripAsync();
        public Task<ReviewDTO> GetReviewByMaxCommentsAsync(Guid userId = default);
        public Task<IEnumerable<TripDTO>> GetMostlyUsedTemplatesAsync(int count);
        public Task<TripDTO> GetTripByMaxReviewAsync(Guid userId = default);
        public Task <IEnumerable<TripDTO>> GetLastTripsByPeriodAsync(DateTime srart, DateTime end);
        public Task <IEnumerable<TripDTO>> GetTripsByLowPriceAsync(int count);
        public Task<int> GetRegisteredUsersByPeriodAsync(DateTime start, DateTime end);
        public Task<int> GetUsersCountTripsDateRangeAsync(DateTime start, DateTime end);
        public Task<TripDTO> GetLongestTripAsync();
        public Task<TripDTO> GetShortestTripAsync();
        public Task<int> GetTripsCreatedByPeriodAsync(DateTime start, DateTime end);




    }
}
