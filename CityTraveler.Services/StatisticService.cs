using CityTraveler.Domain.Entities;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CityTraveler.Domain.Enums;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ILogger<StatisticService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly UserManagementService _userManagementService;
        public StatisticService(ApplicationContext context, IMapper mapper, ILogger<StatisticService> logger, UserManagementService userManagementService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _userManagementService = userManagementService;
        }
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public async Task<int> QuantityPassEntertaiment(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Trips.SelectMany(x => x.Entertaiments).Distinct().Count();

            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get quantity of pass entertaiment {e.Message}");
            }
        }
        public async Task<IEnumerable<TripPrewievDTO>> GetTripVisitEntertaiment(Guid userId,EntertaimentModel entertaiment)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                var trips = user.Trips.Where(x => x.Entertaiments.Contains(entertaiment));
                return trips.Select(x => _mapper.Map<TripModel, TripPrewievDTO>(x));

            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<TripPrewievDTO>();
            }

        }
        public async Task<double> GetAverageRatingUserTrip(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Trips.Average(x => x.AverageRating);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get average rating user trips {e.Message}");
            }
            
        }
        public async Task<double> GetAverageEntertaimentUserTrip(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Trips.Average(x => x.Entertaiments.Count);

            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get average count entertaiment user trip {e.Message}");
            }
        }
        public async Task<TimeSpan> GetMaxTimeUserTrip(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Trips.Max(x => x.TripEnd - x.TripStart);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get maximal time for Trip {e.Message}");
            }
        }
        public async Task<TimeSpan> GetMinTimeUserTrip(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Trips.Min(x => x.TripEnd - x.TripStart);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get minimal time for Trip {e.Message}");
            }
        }
        public async Task<double> GetAveragePriceUserTrip(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Trips.Average(x => x.Price.Value);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get average price of user trips {e.Message}");
            }
        }
        public async Task<int> GetCountPassedUserTrip(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Trips.Count(x => x.TripStatus == TripStatus.Passed);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get count of passed user trip {e.Message}");
            }
        }
        public async Task<IEnumerable<TripPrewievDTO>> GetActivityUserTrip(Guid userId, DateTime timeStart, DateTime timeEnd)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            if (timeEnd < timeStart)
            {
                _logger.LogWarning("TimeStart can`t be more than TimeEnd");
                return null;
            }
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                var trips = user.Trips.Where(x =>
                         x.Created < timeEnd
                         && x.Created > timeStart);
                return trips.Select(x => _mapper.Map<TripModel, TripPrewievDTO>(x));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<TripPrewievDTO>();
            }
          
        }
        public async Task<double> GetAverageAgeUser()
        {
            try
            {
                return _context.UserProfiles.Average(x => DateTime.Today.Year - x.Birthday.Year);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get average age user {e.Message}");
            }
        }
        public async Task<double> GetAvarageEnternaimentInTrip()
        {
            try
            {
               return _context.Trips.Average(x => x.Entertaiments.Count());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get average enternaiment all trip {e.Message}");
            }
        }
        public async Task<double> GetAverageUserReviewRating(Guid userId)
        {
            try
            {
                var user = await _userManagementService.GetUserByIdAsync(userId);
                return user.Reviews.Average(x => x.Rating.Value);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get average rating user reviews {e.Message}");
            }
        }
    }

}
