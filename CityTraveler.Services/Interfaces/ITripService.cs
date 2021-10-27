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

        public Task<bool> DeleteTripAsync(TripModel trip);

        public IEnumerable<TripModel> GetTrips(int skip = 0, int take = 10);

        public TripModel GetTripById(Guid tripId);

        //public IEnumerable<TripModel> GetTripByPlace(Guid placeId);


        public IEnumerable<TripModel> GetTripByDate(DateTime date);

        public IEnumerable<TripModel> GetTripsByAverageRating(double rating);

        public IEnumerable<TripModel> GetTripsByOptimalSpent(TimeSpan optSpent);


        public Task<IEnumerable<TripModel>> GetTripsByEntertainmentAsync(Guid entertainmentId);
        public IEnumerable<TripModel> GetTripsByEntartainmentName(string name);

        public IEnumerable<TripImageModel> GetImagesFromtrip(Guid tripId);


        public IEnumerable<TripModel> GetTripsOrderedByRatingBy();
        public IEnumerable<TripModel> GetTripsOrderdByRatingByDesc();



        public Task<bool> AddNewReviewForTripAsync(ReviewModel newReview);
        public Task<bool> RemoveReviewFForTripAsync(ReviewModel review);
       //public Task<bool> UpdateReviewForTrip(ReviewModel review);

        public IEnumerable<TripModel> GetTripsByStatus(TripStatus status);
        public Task<bool> UpdateTripSatusAsync(Guid tripId, TripStatus newStatus);

        public IEnumerable<TripModel> GetTripsByPrice(double price);
        public IEnumerable<TripModel> OrderTripsByPriceBy();
        public IEnumerable<TripModel> OrderTripsByPriceByDesc();

        public Task<bool> UpdateTripTitleAsync(Guid tripId, string newTitle);
        public Task<bool> UpdateTripDescriptionAsync(Guid tripId, string newDecription);


        public Task<bool> AddEntertainmetToTripAsync(Guid tripId, EntertaimentModel newEntertainment);
        public Task<bool> DeleteEntertainmentFromTrip(Guid tripId, EntertaimentModel entertainment);

        //public TimeSpan SetRealSpent(  );


    }
}
