using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services.Interfaces
{
    public interface IStatisticService : IServiceMetadata
    {
        public Task<int> QuantityPassEntertaiment(Guid userId);
        public Task<IEnumerable<TripDTO>> GetTripVisitEntertaiment(Guid userId, EntertaimentModel entertaiment);
        public Task<double> GetAverageEntertaimentUserTrip(Guid userId);
        public Task<double> GetAverageRatingUserTrip(Guid userId);
        public Task<TimeSpan> GetMaxTimeUserTrip(Guid userId);
        public Task<TimeSpan> GetMinTimeUserTrip(Guid userId);
        public Task<double> GetAveragePriceUserTrip(Guid userId);
        public Task<int> GetCountPassedUserTrip(Guid userId);
        public Task<IEnumerable<TripDTO>> GetActivityUserTrip(Guid userId, DateTime timeStart, DateTime timeEnd);
        public Task<double> GetAverageAgeUser();
        public Task<double> GetAvarageEnternaimentInTrip();
        public Task<double> GetAverageUserReviewRating(Guid userId);
    }
}
