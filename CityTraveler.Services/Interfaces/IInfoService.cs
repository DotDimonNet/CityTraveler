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
        public Task<EntertainmentShowDTO> GetMostPopularEntertaimentInTripsAsync(Guid userId = default);
        public Task<InfoTripDTO> GetMostPopularTripAsync();
        public Task<ReviewDTO> GetReviewByMaxCommentsAsync(Guid userId = default);
        public Task<IEnumerable<InfoTripDTO>> GetMostlyUsedTemplatesAsync(int count);
        public Task<InfoTripDTO> GetTripByMaxReviewAsync(Guid userId = default);
        public Task <IEnumerable<InfoTripDTO>> GetLastTripsByPeriodAsync(DateTime srart, DateTime end);
        public Task <IEnumerable<InfoTripDTO>> GetTripsByLowPriceAsync(int count);
        public Task<int> GetRegisteredUsersByPeriodAsync(DateTime start, DateTime end);
        public Task<int> GetUsersCountTripsDateRangeAsync(DateTime start, DateTime end);
        public Task<InfoTripDTO> GetLongestTripAsync();
        public Task<InfoTripDTO> GetShortestTripAsync();
        public Task<int> GetTripsCreatedByPeriodAsync(DateTime start, DateTime end);




    }
}
