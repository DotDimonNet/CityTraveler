using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CityTraveler.Services
{
    public class TripService : ITripService
    {
        private ApplicationContext _context;
        public TripService(ApplicationContext context)
        {
            _context = context;
        }

        public bool IsActive { get; set; }
        public string Version { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public async Task<bool> AddNewTripAsync(TripModel newTrip)
        {
            try
            {
                _context.Trips.Add(newTrip);
                await _context.SaveChangesAsync();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on adding new trip", e);
            }
            return true;
        }

        public async Task<bool> DeleteTripAsync(Guid tripId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on deleting trip", e);
            }
            return true;
        }

        public TripModel GetTripById(Guid tripId)
        {
            return _context.Trips.FirstOrDefault(x => x.Id == tripId);
        }


        public IEnumerable<TripModel> GetTrips(int skip = 0, int take = 10)
        {
            return _context.Trips.Skip(skip).Take(take);
        }

        public IEnumerable<TripModel> GetTripsByDate(DateTime date)
        {
            return _context.Trips.Where(x => x.TripStart == date);
        }

        public IEnumerable<TripModel> GetTripsByName(string title)
        {
            return _context.Trips.Where(x => x.Title == title);
        }

        public IEnumerable<TripModel> GetTripsByAverageRating(double rating)
        {
            return _context.Trips.Where(x => x.AverageRating == rating);
        }

        public IEnumerable<TripModel> GetTripsByOptimalSpent(TimeSpan optSpent)
        {
            return _context.Trips.Where(x => x.OptimalSpent == optSpent);
        }
        public IEnumerable<TripModel> OrderTripsByOptimalSpentBy()
        {
            return _context.Trips.OrderBy(x => x.OptimalSpent);
        }

        public IEnumerable<TripModel> OrderTripsByOptimalSpentByDesc()
        {
            return _context.Trips.OrderByDescending(x => x.OptimalSpent);
        }
        public async Task<IEnumerable<TripModel>> GetTripsByEntertainmentAsync(Guid entertainmentId)
        {
            var ent = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == entertainmentId);
            return _context.Trips.Where(x => x.Entertaiment == ent);
        }

        public IEnumerable<TripModel> GetTripsByEntartainmentName(string name)
        {
            return _context.Trips.Where(x => x.Title == name);
        }

        public IEnumerable<TripModel> GetTripsOrderedByRatingBy()
        {
            return _context.Trips.OrderBy(x => x.AverageRating);
        }

        public IEnumerable<TripModel> GetTripsOrderdByRatingByDesc()
        {
            return _context.Trips.OrderByDescending(x => x.AverageRating);
        }

        public IEnumerable<TripModel> GetTripsByStatus(TripStatus status)
        {
            return _context.Trips.Where(x => x.TripStatus == status);
        }

        public async Task<bool> UpdateTripSatusAsync(Guid tripId, TripStatus newStatus)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                var status = await _context.TripStatuses.FirstOrDefaultAsync(x => x.Id == newStatus.Id);
                trip.TripStatus = status;
                _context.Update(trip);
                await _context.SaveChangesAsync();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception On  Updating Trip Status!", e);
            }
            return true;
        }

        public IEnumerable<TripModel> GetTripsByPrice(double price)
        {
            return _context.Trips.Where(x => x.Price.Value == price);
        }

        public IEnumerable<TripModel> OrderTripsByPriceBy()
        {
            return _context.Trips.OrderBy(x => x.Price);
        }

        public IEnumerable<TripModel> OrderTripsByPriceByDesc()
        {
            return _context.Trips.OrderByDescending(x => x.Price);
        }

        public async Task<bool> UpdateTripTitleAsync(Guid tripId, string newTitle)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.Title = newTitle;
                _context.Update(trip);
                _context.SaveChanges();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on updating trip title!", e);
            }
            return true;
        }

        public async Task<bool> UpdateTripDescriptionAsync(Guid tripId, string newDecription)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.Description = newDecription;
                _context.Update(trip);
                _context.SaveChanges();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on updating trip description!", e);
            }
            return true;
        }

        public async Task<bool> AddEntertainmetToTripAsync(Guid tripId, EntertaimentModel newEntertainment)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.Entertaiment.Add(newEntertainment);
                _context.Update(trip);
                _context.SaveChanges();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on adding new entertainment to trip!", e);
            }
            return true;
        }
        public async Task<bool> DeleteEntertainmentFromTrip(Guid tripId, EntertaimentModel entertainment)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.Entertaiment.Remove(entertainment);
                _context.Update(trip);
                _context.SaveChanges();

            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on deleting entertainment from trip!", e);
            }
            return true;
        }

        public IEnumerable<TripModel> GetTripsByTag(string tagString)
        {
            return _context.Trips.Where(x=>x.TagSting.ToLower().Contains(tagString.ToLower()));
        }

        public IEnumerable<TripModel> GetDefaultTrips(int skip = 0, int take = 10)
        {
            return _context.Trips.Where(x=>x.DafaultTrip==true).Skip(skip).Take(take);
        }

        public async Task<bool> SetTripAsDefault(Guid tripId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.DafaultTrip =true;
                _context.Update(trip);
                await _context.SaveChangesAsync();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on setting default as defaut trip", e);
            }
            return true;
        }
        public async Task<bool> RemooveTripFromDefault(Guid tripId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.DafaultTrip = false;
                _context.Update(trip);
                await _context.SaveChangesAsync();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception on removing trip from default", e);
            }
            return true;
        }
    }
}
