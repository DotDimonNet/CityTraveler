using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class InfoService : IInfoService
    {
        private ApplicationContext _context;

        public InfoService(ApplicationContext context)
        {
            _context = context;
        }

        public bool IsActive { get ; set ; }
        public string Version { get ; set ; }

        public async Task<EntertaimentModel> GetMostPopularEntertaimentInTrips(Guid userId = default)
        {
            try
            {
                return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips
                    .SelectMany(x => x.Entertaiment).AsEnumerable().OrderByDescending(x => x.Trips.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).Distinct()
                    .SelectMany(x => x.Entertaiment).AsEnumerable().OrderByDescending(x => x.Trips.Count).FirstOrDefault();
             }
            catch(Exception e)
            {
                throw new InfoServiceException("Failed to get the most popular entertaiment in trips");

            }
           
        }
        public async Task<TripModel> GetTripByMaxChoiceOfUsers()
        {
            try
            {
                return await  _context.Trips.OrderByDescending(x => x.Users.Count).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new InfoServiceException("Failed to get the most popular trip ");
            }
            
        }
        public async Task<ReviewModel> GetReviewByMaxComments(Guid userId = default)
        {
            try
            {
                return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Reviews.AsEnumerable().OrderByDescending(x => x.Comments.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Reviews).OrderByDescending(x => x.Comments.Count).AsEnumerable().FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new InfoServiceException("Failed to get the review ");
            }
            
        }

        public async Task<TripModel> GetTripByMaxReview(Guid userId = default)
        {
            try
            {
                return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips.OrderByDescending(x => x.Reviews.Count).AsEnumerable().FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).OrderByDescending(x => x.Reviews.Count).AsEnumerable().FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new InfoServiceException("Failed to get the trip");
            }

        }

        public IEnumerable<TripModel> GetLastTripsByPeriod(DateTime start, DateTime end)
        {
            if (start > end)
                throw new InfoServiceException("Invalid arguments");
            return _context.Trips.Where(x => x.TripStart > start && x.TripStart < end);
        }

        public IEnumerable<TripModel> GetTripsByLowPrice(int count = 3)
        {
            return _context.Trips.OrderBy(x => x.Price.Value).Take(count);
            
        }

        public async Task<int> GetRegisteredUsersByPeriod(DateTime start, DateTime end)
        {
            if (start > end)
                throw new InfoServiceException("Invalid arguments");
            try
            {
                return await _context.Users.CountAsync(x => x.Profile.Created > start && x.Profile.Created < end);
            }
            catch(Exception e)
            {
                throw new InfoServiceException($"Failed to get registered users: {e.Message}");
            }
           
        }
        public IEnumerable<TripModel> GetMostlyUsedTemplates(int count = 5)
        {
            try
            {
                var templateIds = _context.Trips.Select(x => x.TemplateId).GroupBy(x => x)
                    .OrderByDescending(g => g.Count())
                    .Select(x => x.Key)
                    .Take(count);
                return _context.Trips.Where(x => templateIds.Contains(x.Id));
            }
            catch (Exception e)
            {
                throw new InfoServiceException($"Failed to get mostly used templates: {e.Message}");
            }
            
        }

        public int GetUsersCountTripsDateRange(DateTime start, DateTime end)
        {
            if (start > end)
                throw new InfoServiceException("Invalid arguments");
            return _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).SelectMany(x => x.Users).Distinct().Count();
        }

        public async Task<TripModel> GetLongestTrip()
        {
            try
            {
                return await _context.Trips.OrderByDescending(x => x.RealSpent).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new InfoServiceException("The trip not found");
            }
        }

        public async Task<TripModel> GetShortestTrip()
        {
            try
            {
                return await _context.Trips.OrderBy(x => x.RealSpent).FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                throw new InfoServiceException("The trip not found");
            }
          
        }

        public int GetTripsCreatedByPeriod(DateTime start, DateTime end)
        {
            if (start > end)
                throw new InfoServiceException("Invalid arguments");
            return _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).Count();
        }

        
    }
}
