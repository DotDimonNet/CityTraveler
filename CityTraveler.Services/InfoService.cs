﻿using CityTraveler.Domain.Entities;
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
        public InfoService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool IsActive { get ; set ; }
        public string Version { get ; set ; }

        const string messageExceptionObjectNull = "Object not found";
        const string messageExceptionArgument = "Invalid arguments";

        public async Task<EntertainmentShowDTO> GetMostPopularEntertaimentInTripsAsync(Guid userId = default)
        {
            try
            {
                var entertaiment = userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips
                    .SelectMany(x => x.Entertaiment).OrderByDescending(x => x.Trips.Count()).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).Distinct()
                    .SelectMany(x => x.Entertaiment).OrderByDescending(x => x.Trips.Count()).FirstOrDefault();

                if(entertaiment != null)
                {
                    return _mapper.Map<EntertaimentModel, EntertainmentShowDTO>(entertaiment);
                }
                throw new InfoServiceException(messageExceptionObjectNull);
             }
            catch(Exception e)
            {
                _logger.LogError( $"Error:{e.Message}");
                return null;
            }  
        }
        public async Task<TripDTO> GetMostPopularTripAsync()
        {
            try
            {          
                var trip = await _context.Trips.OrderByDescending(x => x.Users.Count).FirstOrDefaultAsync();
                if(trip != null)
                {
                    return _mapper.Map<TripModel, TripDTO>(trip);
                }
                throw new InfoServiceException(messageExceptionObjectNull);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return null;
            }

        }
        public async Task<ReviewDTO> GetReviewByMaxCommentsAsync(Guid userId = default)
        {
            try
            {
                var review = userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Reviews.OrderByDescending(x => x.Comments.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Reviews).OrderByDescending(x => x.Comments.Count).FirstOrDefault();
                
                if(review != null)
                {
                    return _mapper.Map<ReviewModel, ReviewDTO>(review);
                }
                throw new InfoServiceException(messageExceptionObjectNull); 
            } 
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return null;
            }          
        }

        public async Task<TripDTO> GetTripByMaxReviewAsync(Guid userId = default)
        {
            try
            {
                var trip = userId != Guid.Empty
                ? (await _context.Users.FirstOrDefaultAsync(x => x.Id == userId)).Trips.OrderByDescending(x => x.Reviews.Count).FirstOrDefault()
                : _context.Users.SelectMany(x => x.Trips).OrderByDescending(x => x.Reviews.Count).FirstOrDefault();

                if (trip != null)
                {
                    return _mapper.Map<TripModel, TripDTO>(trip);
                }
                throw new InfoServiceException(messageExceptionObjectNull);   
            }
            catch (Exception e)
            {
                _logger.LogError($"Error:{e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<TripDTO>> GetLastTripsByPeriodAsync(DateTime start, DateTime end)
        {
            try
            {
                if (start > end)
                {
                    throw new InfoServiceException(messageExceptionArgument);
                }

                var trips = await Task.Run(() => _context.Trips.Where(x => x.TripStart > start && x.TripStart < end));

                if (trips!= null)
                {
                    return _mapper.Map<IEnumerable<TripModel>, IEnumerable<TripDTO>>(trips);
                }
                throw new InfoServiceException(messageExceptionObjectNull);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<TripDTO>();
            }
            
        }

        public async Task<IEnumerable<TripDTO>> GetTripsByLowPriceAsync(int count)
        {
            try
            {
                var trips =await Task.Run(() => _context.Trips.OrderBy(x => x.Price.Value).Take(count));

                if (trips != null)
                {
                    return _mapper.Map<IEnumerable<TripModel>, IEnumerable<TripDTO>>(trips);
                }

                throw new InfoServiceException(messageExceptionObjectNull);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<TripDTO>();
            }
            
        }

        public async Task<int> GetRegisteredUsersByPeriodAsync(DateTime start, DateTime end)
        {
            try
            {
                if (start > end)
                {
                    throw new InfoServiceException("Invalid arguments");
                }
                var usersCount = await _context.Users.CountAsync(x => x.Profile.Created > start && x.Profile.Created < end);
                if (usersCount != 0)
                {
                    return usersCount;
                }
                throw new InfoServiceException(messageExceptionObjectNull);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return 0;
            }
              
        }
        public async Task<IEnumerable<TripDTO>> GetMostlyUsedTemplatesAsync(int count = 5)
        {
            try
            {
                var templateIds = _context.Trips.Select(x => x.TemplateId).GroupBy(x => x)
                    .OrderByDescending(g => g.Count())
                    .Select(x => x.Key)
                    .Take(count);
                var templatesTriModel =Task.Run(() => _context.Trips.Where(x => templateIds.Contains(x.Id)));

                return _mapper.Map<IEnumerable<TripModel>, IEnumerable<TripDTO>>(await templatesTriModel);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                return Enumerable.Empty<TripDTO>();
            }
            
        }

        public async Task<int> GetUsersCountTripsDateRangeAsync(DateTime start, DateTime end)
        {
        
            if (start > end)
            {
                throw new InfoServiceException(messageExceptionArgument);
            }
                
            return await Task.Run(() => _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).SelectMany(x => x.Users).Distinct().Count());
        }

        public async Task<TripDTO> GetLongestTripAsync()
        {
            try
            {
                var trip = await _context.Trips.OrderByDescending(x => x.RealSpent).FirstOrDefaultAsync();
                if (trip != null)
                {
                    return _mapper.Map<TripModel, TripDTO>(trip);
                }
                throw new InfoServiceException(messageExceptionObjectNull);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e}");
                return null;
            }
        }

        public async Task<TripDTO> GetShortestTripAsync()
        {
            var trip = await _context.Trips.OrderBy(x => x.RealSpent).FirstOrDefaultAsync();
            return _mapper.Map<TripModel, TripDTO>(trip);          
        }

        public async Task<int> GetTripsCreatedByPeriodAsync(DateTime start, DateTime end)
        {
            if (start > end)
            {
                throw new InfoServiceException("Invalid arguments");
            }

            return await Task.Run(() => _context.Trips.Where(x => x.TripStart > start && x.TripEnd < end).Count());
        }
    }
}
