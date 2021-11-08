using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CityTraveler.Domain.DTO;

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
        public async Task<CommentDTO> GetUserLastComment(Guid userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                return _mapper.Map < CommentModel, CommentDTO >(_context.Comments.LastOrDefault(x => x.Owner.UserId == userId));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last user comment {e.Message}");
            }
        }
        public async Task<CommentDTO> GetLastComment()
        {
            try
            {
                return _mapper.Map<CommentModel, CommentDTO>(_context.Comments.LastOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last comment {e.Message}");
            }
        }
        public async Task<ReviewDTO> GetLastReview()
        {
            try
            {
                return _mapper.Map<ReviewModel, ReviewDTO>(_context.Reviews.LastOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last review {e.Message}");
            }
        }
        public async Task<ReviewDTO> GetUserLastReview(Guid userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                return _mapper.Map<ReviewModel, ReviewDTO>(_context.Reviews.LastOrDefault(x => x.UserId == userId));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last user review {e.Message}");
            }
        }
        public async Task<TripDTO> GetLastTrip()
        {
            try
            {
                return _mapper.Map<TripModel, TripDTO>(_context.Trips.LastOrDefault(x => x.TripStatus.Id == 3));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get last trip {e.Message}");
            }
        }
        public async Task<TripDTO> GetUserLastTrip(Guid userId, bool passed = false)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
      
                return passed 
                    ? _mapper.Map<TripModel, TripDTO>(user.Trips.LastOrDefault(x => x.TripStatus.Id == 3))
                    : _mapper.Map<TripModel, TripDTO>(user.Trips.LastOrDefault());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get user last trip {e.Message}");
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
                return user.Trips.SelectMany(x => x.Entertaiments).Distinct().OrderBy(x => x.Created);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Failed to get pass entertaiment {e.Message}");
            }
        }
        public async Task<IEnumerable<CommentDTO>> GetUserComments(Guid userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                _logger.LogWarning("User not found");
                return null;
            }
            try
            {
                var comments = _context.Comments.Where(x => x.Owner.UserId == userId).OrderBy(x => x.Created);
                return comments.Select(x => _mapper.Map<CommentModel, CommentDTO>(x)); ;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<CommentDTO>();
            }
        }
    }
}
