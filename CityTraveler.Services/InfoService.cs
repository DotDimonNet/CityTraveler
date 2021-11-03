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
using AutoMapper;
using Microsoft.Extensions.Logging;
using CityTraveler.Domain.DTO;


namespace CityTraveler.Services
{
    public class InfoService : IInfoService
    {
        private ApplicationContext _context;
        private readonly ILogger<InfoService> _logger;
        private readonly IMapper _mapper;

        public InfoService(ApplicationContext context, IMapper mapper, ILogger<InfoService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public bool IsActive { get ; set ; }
        public string Version { get ; set ; }

        public async Task<EntertainmentShowDTO> GetMostPopularEntertaimentInTrips(Guid userId = default)
        {
            try
            {
                var entertaiment = userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips
                    .SelectMany(x => x.Entertaiment).AsEnumerable().OrderByDescending(x => x.Trips.Count()).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).Distinct()
                    .SelectMany(x => x.Entertaiment).AsEnumerable().OrderByDescending(x => x.Trips.Count()).FirstOrDefault();

                return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertaiment);
             }
            catch(Exception e)
            {
                _logger.LogError( $"Error:{e.Message}");
                throw new InfoServiceException($"Failed to get the most popular entertaiment in trips: {e.Message}");
            }  
        }
        public async Task<TripDTO> GetTripByMaxChoiceOfUsers()
        {
            if(_context.Trips.OrderByDescending(x => x.Users.Count).FirstOrDefault() == null)
            {
                throw new InfoServiceException($"Trip is not found");
            }

            var trip = await  _context.Trips.OrderByDescending(x => x.Users.Count).FirstOrDefaultAsync();
            
            return _mapper.Map<TripModel, TripDTO>(trip);     
        }
        public async Task<ReviewDTO> GetReviewByMaxComments(Guid userId = default)
        {
            try
            {
                var review = userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Reviews.AsEnumerable().OrderByDescending(x => x.Comments.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Reviews).OrderByDescending(x => x.Comments.Count).AsEnumerable().FirstOrDefault();
                
                return _mapper.Map<ReviewModel, ReviewDTO>(review);
            }
                
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new InfoServiceException($"Failed to get the review: {e.Message}");
            }          
        }

        public async Task<TripDTO> GetTripByMaxReview(Guid userId = default)
        {
            try
            {
                var trip = userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips.OrderByDescending(x => x.Reviews.Count).AsEnumerable().FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).OrderByDescending(x => x.Reviews.Count).AsEnumerable().FirstOrDefault();
               
                return _mapper.Map<TripModel, TripDTO>(trip);

            }
   
            catch (Exception e)
            {
                _logger.LogError($"Error:{e.Message}");
                throw new InfoServiceException($"Failed to get the trip: {e.Message}");
            }
        }

        public IEnumerable<TripDTO> GetLastTripsByPeriod(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InfoServiceException("Invalid arguments");
            }
                
            var trips = _context.Trips.Where(x => x.TripStart > start && x.TripStart < end);
            
            return _mapper.Map<IEnumerable<TripModel>, IEnumerable<TripDTO>>(trips);
        }

        public IEnumerable<TripDTO> GetTripsByLowPrice(int count = 3)
        {
            var trips = _context.Trips.OrderBy(x => x.Price.Value).Take(count);
           
            return _mapper.Map<IEnumerable<TripModel>, IEnumerable<TripDTO>>(trips);
        }

        public async Task<int> GetRegisteredUsersByPeriod(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InfoServiceException("Invalid arguments");
            }        
            return await _context.Users.CountAsync(x => x.Profile.Created > start && x.Profile.Created < end);    
        }
        public IEnumerable<TripDTO> GetMostlyUsedTemplates(int count = 5)
        {
            try
            {
                var templateIds = _context.Trips.Select(x => x.TemplateId).GroupBy(x => x)
                    .OrderByDescending(g => g.Count())
                    .Select(x => x.Key)
                    .Take(count);
                var templatesTriModel = _context.Trips.Where(x => templateIds.Contains(x.Id));

                return _mapper.Map<IEnumerable<TripModel>, IEnumerable<TripDTO>>(templatesTriModel);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new InfoServiceException($"Failed to get mostly used templates: {e.Message}");
            }
            
        }

        public int GetUsersCountTripsDateRange(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InfoServiceException("Invalid arguments");
            }
                
            return _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).SelectMany(x => x.Users).Distinct().Count();
        }

        public async Task<TripDTO> GetLongestTrip()
        {
            var trip = await _context.Trips.OrderByDescending(x => x.RealSpent).FirstOrDefaultAsync();

            return _mapper.Map<TripModel, TripDTO>(trip);
        }

        public async Task<TripDTO> GetShortestTrip()
        {
            var trip = await _context.Trips.OrderBy(x => x.RealSpent).FirstOrDefaultAsync();
            return _mapper.Map<TripModel, TripDTO>(trip);          
        }

        public int GetTripsCreatedByPeriod(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InfoServiceException("Invalid arguments");
            }

            return _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).Count();
        }
    }
}
