using CityTraveler.Domain.Entities;
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
        ApplicationContext _context;

        public InfoService(ApplicationContext context)
        {
            _context = context;
        }

        public bool IsActive { get ; set ; }
        public string Version { get ; set ; }

        public async Task<EntertaimentModel> GetMostPopularEntertaimentInTrips(Guid userId = default)
        {
           return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips
                    .SelectMany(x => x.Entertaiment).AsEnumerable().GroupBy(x => x.Trips.Count).FirstOrDefault()?.FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).Distinct()
                    .SelectMany(x => x.Entertaiment).AsEnumerable().OrderBy(x => x.Trips.Count).FirstOrDefault();
        }
        public async Task<TripModel> GetTripByMaxChoiceOfUsers()
        {
            return await  _context.Trips.OrderBy(x => x.Users.Count).FirstOrDefaultAsync();
        }
        public async Task<ReviewModel> GetReviewByMaxComment(Guid userId = default)
        {
            return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Reviews.AsEnumerable().OrderBy(x => x.Comments.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Reviews).OrderBy(x => x.Comments.Count).AsEnumerable().FirstOrDefault();
        }

        public async Task<TripModel> GetTripByMaxReview(Guid userId = default)
        {
            return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips.OrderBy(x => x.Reviews.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).OrderBy(x => x.Reviews.Count).FirstOrDefault();
        }

        public IEnumerable<TripModel> GetLastTripsByPeriod(DateTime srart, DateTime end)
        {
            return _context.Trips.Where(x => x.TripStart > srart && x.TripStart < end);
        }
        public async Task<TripModel> GetTripByLowPrice()
        {
            return await _context.Trips.OrderBy(x => x.Price).FirstOrDefaultAsync();
            
        }

        public async Task<int> GetRegisteredUserByPeriod(DateTime start, DateTime end)
        {
           return await _context.Users.CountAsync(x => x.Profile.Created > start && x.Profile.Created < end);
        }
        public async Task<TripModel> GetMostlyUsedTemplate()
        {
            var templateId = _context.Trips.Select(x => x.TemplateId).GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
            return await _context.Trips.FirstOrDefaultAsync(x => x.Id == templateId);
        }

        public int GetUsersCountTripsDateRange(DateTime start, DateTime end)
        {
            return _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).SelectMany(x => x.Users).Distinct().Count();
        }

        public async Task<TripModel> GetLongestTrip()
        {
            return await _context.Trips.OrderByDescending(x => x.RealSpent).FirstOrDefaultAsync();
        }

        public async Task<TripModel> GetShortestTrip()
        {
            return await _context.Trips.OrderBy(x => x.RealSpent).FirstOrDefaultAsync();
        }

        public int GetTripsCreatedByPeriod(DateTime start, DateTime end)
        {
            return _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).Count();
        }

        
    }
}
