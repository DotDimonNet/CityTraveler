using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Extensions;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CityTraveler.Services
{
    public class TripService : ITripService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<TripService> _logger;
        private readonly IMapper _mapper;
        public TripService(ApplicationContext context, IMapper mapper, ILogger<TripService> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public bool IsActive { get; set; }
        public string Version { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public async Task<bool> AddNewTripAsync(AddNewTripDTO newTrip)
        {
            try
            {
                var model = _mapper.Map<AddNewTripDTO, TripModel>(newTrip);
                _context.Trips.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on adding new trip! {e.Message}");
            }
        }

        public async Task<bool> DeleteTripAsync(Guid tripId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                _context.Trips.Remove(trip);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on deleting trip! {e.Message}");
            }
        }

        public async Task<bool> AddDefaultTrip(DefaultTripDTO newDefaultTrip)
        {
            try
            {
                var model = _mapper.Map<DefaultTripDTO, TripModel>(newDefaultTrip);
                _context.Trips.Add(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on adding new default trip! {e.Message}");
            }

        }

        public IEnumerable<DefaultTripDTO> GetDefaultTrips(int skip = 0, int take = 10)
        {
            var trips = _context.Trips.Where(x => x.DafaultTrip == true).Skip(skip).Take(take);
            return trips.Select(x => _mapper.Map<TripModel, DefaultTripDTO>(x));
        }   

        public DefaultTripDTO GetDefaultTripById(Guid defaltTripId)
        {
            var trip = _context.Trips.FirstOrDefault(x => x.Id == defaltTripId);
            return _mapper.Map<TripModel, DefaultTripDTO>(trip);
        }

        public async Task<bool> SetTripAsDefault(Guid tripId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.DafaultTrip = true;
                _context.Update(trip);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on setting default as defaut trip! {e.Message}");
            }
        }

        public async Task<bool> RemoveTripFromDefault(Guid tripId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.DafaultTrip = false;
                _context.Update(trip);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on removing trip from default! {e.Message}");
            }
        }

        public TripDTO GetTripById(Guid tripId)
        {
            var trip = _context.Trips.FirstOrDefault(x => x.Id == tripId);
            return _mapper.Map<TripModel, TripDTO>(trip);       
        }

        public IEnumerable<TripDTO> GetTrips(string title, double rating, TimeSpan optimalSpent, double price, string tag, int skip = 0, int take = 10)
        {         
            var trips = _context.Trips.Skip(skip)
                .Take(take)
                .Where(x => x.Title.Contains("") && x.AverageRating == rating 
                && x.OptimalSpent == optimalSpent && x.Price.Value == price 
                && x.TagSting.Contains(""));
       
            return trips.Select(x => _mapper.Map<TripModel, TripDTO>(x));            
        }

        public IEnumerable<TripModel> GetTripsByName(string tripName)
        {
            return _context.Trips.Where(x => x.Title == tripName);
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
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on  updating trip status! {e.Message}");
            }
        }

        public async Task<bool> UpdateTripTitleAsync(Guid tripId, string newTitle)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.Title = newTitle;
                _context.Update(trip);
                _context.SaveChanges();

                _logger.LogInformation($"Trip with id:{tripId} was updated.");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on updating trip title! {e.Message}");
            }       
        }

        public async Task<bool> UpdateTripDescriptionAsync(Guid tripId, string newDecription)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.Description = newDecription;
                _context.Update(trip);
                _context.SaveChanges();

                _logger.LogInformation($"Trip with id:{tripId} was updated.");
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on updating trip description! {e.Message}");
            }      
        }

        public async Task<bool> AddEntertainmetToTripAsync(Guid tripId, EntertainmentGetDTO newEntertainment)
        {
            try
            {
                var model = _mapper.Map<EntertainmentGetDTO, EntertaimentModel>(newEntertainment);
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                trip.Entertaiment.Add(model);
                _context.Update(trip);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Exception on adding new entertainment to trip! {e.Message}");
            }
        }

        public async Task<bool> DeleteEntertainmentFromTrip(Guid tripId, Guid entertainmentId)
        {
            try
            {
                var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == tripId);
                var entertainment = await _context.Entertaiments.FirstOrDefaultAsync(x => x.Id == entertainmentId);
                trip.Entertaiment.Remove(entertainment);
                _context.Update(trip);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new TripServiceException($"Exception on deleting entertainment from trip! {e.Message}");
            }
        }
    }
}
