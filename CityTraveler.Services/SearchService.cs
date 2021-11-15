using CityTraveler.Domain.Entities;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.Enums;
using CityTraveler.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityTraveler.Domain.DTO;
using AutoMapper;

namespace CityTraveler.Services
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<SearchService> _logger;
        private readonly IMapper _mapper;

        public SearchService(ApplicationContext dbContext, IMapper mapper, ILogger<SearchService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        /*public async Task<IEnumerable<EntertainmentGetDTO>> FilterEntertainments(FilterEntertainment filter)
        {
            switch (filter)
            {
                case var fil when fil.PriceLess < fil.PriceMore:
                    _logger.LogWarning("PriceMore can`t be more than priceLess.");
                    return Enumerable.Empty<EntertainmentGetDTO>();
                case var fil when fil.RatingLess < fil.RatingMore:
                    _logger.LogWarning("RatingMore can`t be more than ratingLess.");
                    return Enumerable.Empty<EntertainmentGetDTO>();
                case var fil when fil.RatingLess < 0:
                    _logger.LogWarning("RatingLess can`t be less than 0.");
                    return Enumerable.Empty<EntertainmentGetDTO>();
                case var fil when fil.RatingMore < 0:
                    _logger.LogWarning("RatingMore can`t be less than 0.");
                    return Enumerable.Empty<EntertainmentGetDTO>();
                case var fil when fil.PriceLess < 0:
                    _logger.LogWarning("PriceLess can`t be less than 0.");
                    return Enumerable.Empty<EntertainmentGetDTO>();
                case var fil when fil.PriceMore < 0:
                    _logger.LogWarning("PriceMore can`t be less than 0.");
                    return Enumerable.Empty<EntertainmentGetDTO>();
                default:
                    break;
            }
            try
            {
                var isEntetainmentType = (filter.Type == -1);
                var tripIds = _dbContext.Trips.Where(x => x.Title.Contains(filter.TripName)).Select(x => x.Id);
                var result = await Task.Run(()=>_dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title ?? "")
                            && x.Address.Street.Title.Contains(filter.StreetName ?? "")
                            && x.Address.HouseNumber.Contains(filter.HouseNumber ?? "")
                            && ( isEntetainmentType || (EntertainmentType) filter.Type == x.Type)
                            //&& x.Trips.Any(x => tripIds.Contains(x.Id))
                            && x.AveragePrice.Value >= filter.PriceMore
                            && x.AveragePrice.Value <= filter.PriceLess
                            && x.AverageRating >= filter.RatingMore
                            && x.AverageRating <= filter.RatingLess));
                return _mapper.Map<IEnumerable<EntertaimentModel>, IEnumerable<EntertainmentGetDTO>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to filter entertainments {e.Message}");
                return Enumerable.Empty<EntertainmentGetDTO>();
            }
        }

        private Task GetTripByName(string tripName)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TripDTO>> FilterTrips(FilterTrips filter)
        {
            switch (filter)
            {
                case var fil when fil.PriceLess < fil.PriceMore:
                    _logger.LogWarning("PriceMore can`t be more than priceLess. ");
                    return Enumerable.Empty<TripDTO>();
                case var fil when fil.AverageRatingLess < fil.AverageRatingMore:
                    _logger.LogWarning("RatingMore can`t be more than ratingLess.");
                    return Enumerable.Empty<TripDTO>();
                case var fil when fil.AverageRatingLess < 0:
                    _logger.LogWarning("AverageRatingLess can`t be less than 0.");
                    return Enumerable.Empty<TripDTO>();
                case var fil when fil.AverageRatingMore < 0:
                    _logger.LogWarning("AverageRatingMore can`t be less than 0.");
                    return Enumerable.Empty<TripDTO>();
                case var fil when fil.PriceLess < 0:
                    _logger.LogWarning("PriceLess can`t be less than 0.");
                    return Enumerable.Empty<TripDTO>();
                case var fil when filter.PriceMore < 0:
                    _logger.LogWarning("PriceMore can`t be less than 0.");
                    return Enumerable.Empty<TripDTO>();
                default:
                    break;
            }
            try
            {
                bool isTripStatus = filter.TripStatus == -1;
                var result = await Task.Run(()=>_dbContext.Trips.Where(x =>
                    x.Description.Contains(filter.Description)
                    && x.TripEnd >= filter.TripEnd
                    && x.TripStart >= filter.TripStart
                    && x.RealSpent >= filter.RealSpent
                    && x.OptimalSpent >= filter.OptimalSpent
                    && ( isTripStatus || (TripStatus)filter.TripStatus == x.TripStatus)
                    && x.Users.Any(x => x.Profile.Name.StartsWith(filter.User))
                    && x.Entertaiments.Any(x => x.Title.StartsWith(filter.EntertaimentName))
                    && x.Title.Contains(filter.Title)
                    && x.Price.Value >= filter.PriceMore
                    && x.Price.Value <= filter.PriceLess
                    && x.AverageRating >= filter.AverageRatingMore
                    && x.AverageRating <= filter.AverageRatingLess));
                return _mapper.Map<IEnumerable<TripModel>, IEnumerable<TripDTO>>(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to filter trips {e.Message}");
                return Enumerable.Empty<TripDTO>();
            }
        }

        public async Task<IEnumerable<UserDTO>> FilterUsers(FilterUsers filter)
        {
            try
            {
                var tripsIds = _dbContext.Entertaiments
                    .Where(x => x.Title.Contains(filter.EntertainmentName))
                    .SelectMany(x => x.Trips)
                    .Select(x => x.Id);

                var isAnyTrips = tripsIds.Any();

                var result = await Task.Run(() => _dbContext.Users.Where(x =>
                        x.UserName.Contains(filter.UserName)
                        && x.Profile.Gender.Contains(filter.Gender)
                        && (isAnyTrips || x.Trips.Where(x => tripsIds.Contains(x.Id)).Any())));

                return _mapper.Map<IEnumerable<UserDTO>>(result);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter users {e.Message}");
                return Enumerable.Empty<UserDTO>();
            }
        }
    }
}