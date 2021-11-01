using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IStatisticService
    {
        public Task<int> QuantityPassEntertaiment(Guid UserID);
        public Task<IEnumerable<TripModel>> GetTripVisitEntertaiment(Guid UserID, EntertaimentModel rev);
        public Task<double> GetAverageEntertaimentUserTrip(Guid UserID);
        public Task<double> GetAverageRatingUserTrip(Guid userId);
        public Task<TimeSpan> GetAverageTimeUserTrip(Guid UserID);
        public Task<double> GetAveragePriceUserTrip(Guid UserID);
        public Task<int> GetCountPassedUserTrip(Guid UserID);
        public Task<IEnumerable<TripModel>> GetActivityUserTrip(Guid UserID, DateTime time);
        public Task<double> GetAverageAgeUser();
        public Task<double> GetAvarageEnternaimentInTrip();
    }
}
