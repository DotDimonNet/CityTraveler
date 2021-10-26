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
    class InfoService : IInfoService
    {
        ApplicationContext _context;

        public InfoService(ApplicationContext context)
        {
            _context = context;
        }

        public bool IsActive { get ; set ; }
        public string Version { get ; set ; }

        public async Task<EntertaimentModel> GetEntertaimentByMaxChoiceInTrip(Guid userId = default)
        {
            return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips
                    .SelectMany(x => x.Entertaiment).GroupBy(x => x.Trips.Count).FirstOrDefault().FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).Distinct()
                    .SelectMany(x => x.Entertaiment).GroupBy(x => x.Trips.Count).FirstOrDefault().FirstOrDefault();
        }
        public TripModel GetTripByMaxChoiceOfUsers()
        {
            return  _context.Trips.OrderBy(x => x.Users.Count).FirstOrDefault();
        }
        public async Task<ReviewModel> GetReviewByMaxComment(Guid userId = default)
        {
            return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Reviews.OrderBy(x => x.Comments.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Reviews).OrderBy(x => x.Comments.Count).FirstOrDefault();
        }

        public async Task<TripModel> GetTripByMaxReview(Guid userId = default)
        {
            return userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips.OrderBy(x => x.Reviews.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).OrderBy(x => x.Reviews.Count).FirstOrDefault();
        }

        public IEnumerable<TripModel> GetLastTripsByPeriod(DateTime srart, DateTime end)
        {
            return  _context.Trips.Where(x => x.TripStart == srart && x.TripStart == end);
        }
        public TripModel GetTripByLowPrice()
        {
            return _context.Trips.OrderByDescending(x => x.Price).FirstOrDefault();
        }

        public int GetRegisteredUserByPeriod(DateTime start, DateTime end)
        {
           return  _context.Users.Count(x => x.Profile.Created > start && x.Profile.Created < end);
        }
        public async Task<TripModel> GetMostlyUsedTemplate()
        {
            var templateId = _context.Trips.Select(x => x.TemplateId).GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
            return await _context.Trips.FirstOrDefaultAsync(x => x.Id == templateId);
        }

        public int GetUsersTripsDateRange(DateTime start, DateTime end)
        {
            return _context.Trips.Where(x => x.TripStart == start && x.TripEnd == end).Select(x => x.Users).Count();
        }

        public TripModel GetLongestTrip()
        {
            throw new NotImplementedException();
        }
    }
}
