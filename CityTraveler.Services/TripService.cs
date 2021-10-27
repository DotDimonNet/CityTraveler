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
                throw new TripServiceException("Exception on Adding New Trip", e);
            }
            return true;
        }

        public async Task<bool> DeleteTripAsync(TripModel trip)
        {
            try
            {
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
                
            }
            catch (TripServiceException e)
            {

                throw new TripServiceException("Exception On Deleting Trip", e);
            }
            return true;
        }

        public TripModel GetTripById(Guid tripId)         
        {
            return(TripModel) _context.Trips.Where(x => x.Id == tripId);
        }


        public IEnumerable<TripModel> GetTrips(int skip = 0, int take = 10)
        {
            return _context.Trips.Skip(skip).Take(take);
        }

        public IEnumerable<TripModel> GetTripByDate(DateTime date)
        {

            return _context.Trips.Where(x => x.TripStart == date);
        }


        public IEnumerable<TripModel> GetTripsByAverageRating(double rating)
        {
            return _context.Trips.Where(x => x.AverageRating == rating);
            
        }

        public IEnumerable<TripModel> GetTripsByOptimalSpent(TimeSpan optSpent)
        {
            return _context.Trips.Where(x => x.OptimalSpent == optSpent);          
        }

        public IEnumerable<TripModel> GetTripByPlace(EntertaimentModel entertaiment)
        {
            return _context.Trips.Where(x => x.Entertaiment == entertaiment);
             
        }


        public async Task<IEnumerable<TripModel>> GetTripsByEntertainmentAsync(Guid entertainmentId)
        {
            var ent = await _context.Entertaiments.FirstOrDefaultAsync(x=>x.Id==entertainmentId);
            return _context.Trips.Where(x => x.Entertaiment == ent);
        }

        public IEnumerable<TripModel> GetTripsByEntartainmentName(string name)
        {
            return _context.Trips.Where(x => x.Title == name);
             
        }


        public IEnumerable<TripImageModel> GetImagesFromtrip(Guid tripId)
        {

            var trip = _context.Trips.FirstOrDefault(x=>x.Id==tripId);
            return trip.Images;
        }

 


        public IEnumerable<TripModel> GetTripsOrderedByRatingBy()
        {
            return _context.Trips.OrderBy(x => x.AverageRating);
            
        }

        public IEnumerable<TripModel> GetTripsOrderdByRatingByDesc()
        {
            return _context.Trips.OrderByDescending(x => x.AverageRating);
        }

        public async Task<bool> AddNewReviewForTripAsync(ReviewModel newReview)
        {
            try
            {
                _context.Reviews.Add(newReview);
                await _context.SaveChangesAsync();
                
            }
            catch (TripServiceException e)
            {

                throw new TripServiceException("Exception On Adding Review For Trip!", e);
            }
            return true;
        }

        public async Task<bool> RemoveReviewFForTripAsync(ReviewModel review)
        {
            try
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            catch (TripServiceException e)
            {
                throw new TripServiceException("Exception On Removing Trip's Review!", e);
                
            }
            return true;
        }

      
        public IEnumerable<TripModel> GetTripsByStatus(TripStatus status)
        {
            return _context.Trips.Where(x => x.TripStatus == status);
        }

        public async Task<bool> UpdateTripSatusAsync(Guid tripId, TripStatus newStatus)
        {
            try
            {

                var trip = await _context.Trips.FirstOrDefaultAsync(x=>x.Id==tripId);
                var status = await  _context.TripStatuses.FirstOrDefaultAsync(x=>x.Id==newStatus.Id);

                trip.TripStatus = status;
                _context.Update<TripModel>(trip);
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
                var trip = await _context.Trips.FirstOrDefaultAsync(x=>x.Id==tripId);
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
                var trip = await _context.Trips.FirstOrDefaultAsync(x=>x.Id==tripId);
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

        public async  Task<bool> AddEntertainmetToTripAsync(Guid tripId, EntertaimentModel newEntertainment)
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
    }
}
