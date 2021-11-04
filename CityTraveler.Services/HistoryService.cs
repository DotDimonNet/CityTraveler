using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace CityTraveler.Services 
{
    public class HistoryService : IHistoryService
    {
        private readonly ILogger<HistoryService> _logger;
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        public HistoryService(ApplicationContext context, IMapper mapper, ILogger<HistoryService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public async Task<CommentModel> GetUserLastComment(Guid userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                return _context.Comments.LastOrDefault(x => x.Owner.UserId == userId);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last user comment {e.Message}");
            }
        }
        public async Task<CommentModel> GetLastComment()
        {
            try
            {
                return _context.Comments.LastOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last comment {e.Message}");
            }
        }
        public async Task<ReviewModel> GetLastReview()
        {
            try
            {
                return _context.Reviews.LastOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last review {e.Message}");
            }
        }
        public async Task<ReviewModel> GetUserLastReview(Guid userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                return user.Reviews.LastOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last user review {e.Message}");
            }
        }
        public async Task<TripModel> GetLastTrip()
        {
            try
            {
                return _context.Trips.LastOrDefault(x => x.TripStatus.Id == 3);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last trip {e.Message}");
            }
        }
        public async Task<TripModel> GetUserLastTrip(Guid userId, bool passed = false)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                if(passed)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                    return user.Trips.LastOrDefault(x => x.TripStatus.Id == 3);
                }
                else
                {
                    var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                    return user.Trips.LastOrDefault();
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last user trip {e.Message}");
            }
        }
        public async Task<IEnumerable<EntertaimentModel>> GetVisitEntertaiment(Guid userId, bool withoutReview = false)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
                return user.Trips.SelectMany(x => x.Entertaiment).Distinct().OrderBy(x => x.Created);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get pass entertaiment {e.Message}");
            }
        }
        public async Task<IEnumerable<CommentModel>> GetUserComments(Guid userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                return _context.Comments.Where(x => x.Owner.UserId == userId).OrderBy(x => x.Created);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last user comment {e.Message}");
            }
        }
    }
}
