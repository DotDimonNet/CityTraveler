using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ITripService : IServiceMetadata
    {
        public Task<bool> AddNewTripAsync(TripModel newTrip);

        public Task<bool> DeleteTripAsync(Guid tripId);

        public IEnumerable<TripModel> GetTrips(int skip = 0, int take = 10);

        public TripModel GetTripById(Guid tripId);

        public IEnumerable<TripModel> GetTripsByDate(DateTime date);

        public IEnumerable<TripModel> GetTripsByName(string title);

        public IEnumerable<TripModel> GetTripsByAverageRating(double rating);

        public IEnumerable<TripModel> GetTripsByOptimalSpent(TimeSpan optSpent);

        public IEnumerable<TripModel> OrderTripsByOptimalSpentBy();
        public IEnumerable<TripModel> OrderTripsByOptimalSpentByDesc();


        public Task<IEnumerable<TripModel>> GetTripsByEntertainmentAsync(Guid entertainmentId);
        public IEnumerable<TripModel> GetTripsByEntartainmentName(string name);
   
        public IEnumerable<TripModel> GetTripsOrderedByRatingBy();
        public IEnumerable<TripModel> GetTripsOrderdByRatingByDesc();

        public IEnumerable<TripModel> GetTripsByStatus(TripStatus status);
        public Task<bool> UpdateTripSatusAsync(Guid tripId, TripStatus newStatus);

        public IEnumerable<TripModel> GetTripsByPrice(double price);
        public IEnumerable<TripModel> OrderTripsByPriceBy();
        public IEnumerable<TripModel> OrderTripsByPriceByDesc();

        public Task<bool> UpdateTripTitleAsync(Guid tripId, string newTitle);
        public Task<bool> UpdateTripDescriptionAsync(Guid tripId, string newDecription);
        public Task<bool> AddEntertainmetToTripAsync(Guid tripId, EntertaimentModel newEntertainment);
        public Task<bool> DeleteEntertainmentFromTrip(Guid tripId, EntertaimentModel entertainment);

        public IEnumerable<TripModel> GetTripsByTag(string tagString);

        public IEnumerable<TripModel> GetDefaultTrips(int skip = 0, int take=10);
        public Task<bool> SetTripAsDefault(Guid tripId);
        public Task<bool> RemooveTripFromDefault(Guid tripId);

    }
}
