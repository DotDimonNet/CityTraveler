using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ITripService 
    {
        public Task<bool> AddNewTripAsync(AddNewTripDTO newTrip);
        public Task<bool> DeleteTripAsync(Guid tripId);
        public Task<bool> AddDefaultTrip(DefaultTripDTO newDefaultTrip);
        public IEnumerable<TripDTO> GetTrips(string title, double rating, TimeSpan optimalSpent, double price, string tag, int skip = 0, int take = 10);
        public TripDTO GetTripById(Guid tripId);
        public IEnumerable<TripModel> GetTripsByStatus(TripStatus status);
        public Task<bool> UpdateTripSatusAsync(Guid tripId, TripStatus newStatus);
        public Task<bool> UpdateTripTitleAsync(Guid tripId, string newTitle);
        public Task<bool> UpdateTripDescriptionAsync(Guid tripId, string newDecription);
        public Task<bool> AddEntertainmetToTripAsync(Guid tripId, EntertainmentGetDTO newEntertainment);
        public Task<bool> DeleteEntertainmentFromTrip(Guid tripId, Guid entertainmentId);
        public IEnumerable<DefaultTripDTO> GetDefaultTrips(int skip = 0, int take=10);
        public DefaultTripDTO GetDefaultTripById(Guid defaltTripId);
    }
}
