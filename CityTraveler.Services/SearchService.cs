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

        public async Task<IEnumerable<EntertainmentGetDTO>> FilterEntertainments(FilterEntertainment filter)
        {
            if (filter.PriceLess < filter.PriceMore)
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess.");
                return Enumerable.Empty<EntertainmentGetDTO>();
            }
            if (filter.RatingLess < filter.RatingMore)
            {
                _logger.LogWarning("RatingMore can`t be more than ratingLess.");
                return Enumerable.Empty<EntertainmentGetDTO>();
            }
            if (filter.RatingLess < 0)
            {
                _logger.LogWarning("RatingLess can`t be less than 0.");
                return Enumerable.Empty<EntertainmentGetDTO>();
            }
            if (filter.RatingMore < 0)
            {
                _logger.LogWarning("RatingMore can`t be less than 0.");
                return Enumerable.Empty<EntertainmentGetDTO>();
            }
            if (filter.PriceLess < 0)
            {
                _logger.LogWarning("PriceLess can`t be less than 0.");
                return Enumerable.Empty<EntertainmentGetDTO>();
            }
            if (filter.PriceMore < 0)
            {
                _logger.LogWarning("PriceMore can`t be less than 0.");
                return Enumerable.Empty<EntertainmentGetDTO>();
            }
            var trips = await GetTripByName(filter.TripName);

            try
            {
                var result = await Task.Run(() => filter.Type != -1 ?
                              _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title)
                            && x.Address.Street.Title.Contains(filter.StreetName)
                            && x.Address.HouseNumber.Contains(filter.HouseNumber)
                            && x.Type == (EntertainmentType)filter.Type
                            && x.Trips.Where(x => trips.Contains(x)).Any()
                            && x.AveragePrice.Value >= filter.PriceMore
                            && x.AveragePrice.Value <= filter.PriceLess
                            && x.AverageRating >= filter.RatingMore
                            && x.AverageRating <= filter.RatingLess) :
                            _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title)
                            && x.Address.Street.Title.Contains(filter.StreetName)
                            && x.Address.HouseNumber.Contains(filter.HouseNumber)
                            && x.Trips.Any(x => trips.Contains(x))
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

        public async Task<IEnumerable<TripDTO>> FilterTrips(FilterTrips filter)
        {
            if (filter.PriceLess < filter.PriceMore)
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess. ");
                return Enumerable.Empty<TripDTO>();
            }
            if (filter.AverageRatingLess < filter.AverageRatingMore)
            {
                _logger.LogWarning("RatingMore can`t be more than ratingLess.");
                return Enumerable.Empty<TripDTO>();
            }

            if (filter.AverageRatingLess < 0)
            {
                _logger.LogWarning("AverageRatingLess can`t be less than 0.");
                return Enumerable.Empty<TripDTO>();
            }
            if (filter.AverageRatingMore < 0)
            {
                _logger.LogWarning("AverageRatingMore can`t be less than 0.");
                return Enumerable.Empty<TripDTO>();
            }
            if (filter.PriceLess < 0)
            {
                _logger.LogWarning("PriceLess can`t be less than 0.");
                return Enumerable.Empty<TripDTO>();
            }
            if (filter.PriceMore < 0)
            {
                _logger.LogWarning("PriceMore can`t be less than 0.");
                return Enumerable.Empty<TripDTO>();
            }

            try
            {
                var users = await GetUsersByName(filter.User ?? "");
                var entertainment = _entertainmentService.GetEntertainmentsByTitle(filter.EntertaimentName);
                var result = await Task.Run(() => filter.TripStatus != -1
                    ? _dbContext.Trips.Where(x =>
                        x.Description.Contains(filter.Description)
                        && x.TripEnd >= filter.TripEnd
                        && x.TripStart >= filter.TripStart
                        && x.RealSpent >= filter.RealSpent
                        && x.OptimalSpent >= filter.OptimalSpent
                        && x.TripStatus.Id == filter.TripStatus
                        && x.Users.Where(x => users.Contains(x)).Any()
                        && x.Entertaiment.Where(x => entertainment.Contains(x)).Any()
                        && x.Title.Contains(filter.Title)
                        && x.Price.Value >= filter.PriceMore
                        && x.Price.Value <= filter.PriceLess
                        && x.AverageRating >= filter.AverageRatingMore
                        && x.AverageRating <= filter.AverageRatingLess)
                    : _dbContext.Trips.Where(x =>
                          x.Description.Contains(filter.Description)
                          && x.TripEnd >= filter.TripEnd
                          && x.TripStart >= filter.TripStart
                          && x.RealSpent >= filter.RealSpent
                          && x.OptimalSpent >= filter.OptimalSpent
                          && x.Users.Where(x => users.Contains(x)).Count() != -1
                          && x.Entertaiment.Where(x => entertainment.Contains(x)).Any()
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
                var entertainmentsIds = _dbContext.Entertaiments
                    .Where(x => x.Title.Contains(filter.EntertainmentName))
                    .Select(x => x.Id);

                var tripsIds = _dbContext.Entertaiments
                    .Where(x => entertainmentsIds.Contains(x.Id))
                    .SelectMany(x => x.Trips).Select(x => x.Id);

                var result = await Task.Run(() => tripsIds.Any()
                    ? _dbContext.Users.Where(x =>
                        x.UserName.Contains(filter.UserName)
                        && x.Profile.Gender.Contains(filter.Gender)
                        && x.Trips.Any(x => tripsIds.Contains(x.Id)))
                    : _dbContext.Users.Where(x =>
                        x.UserName.Contains(filter.UserName)
                        && x.Profile.Gender.Contains(filter.Gender)));
                return _mapper.Map<IEnumerable<ApplicationUserModel>, IEnumerable<UserDTO>>(result);
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter users {e.Message}");
                return Enumerable.Empty<UserDTO>();
            }
        }

        public async Task<IEnumerable<ApplicationUserModel>> GetUsersByName(string name = "")
        {
            try
            {
                return await Task.Run(() => _dbContext.Users.Where(x => x.Profile.Name.Contains(name)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get users by name {e.Message}");
                return Enumerable.Empty<ApplicationUserModel>();
            }
        }

        public async Task<IEnumerable<TripModel>> GetTripByName(string name = "")
        {
            try
            {
                return await Task.Run(() => _dbContext.Trips.Where(x => x.Title.Contains(name)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get trips by name {e.Message}");
                return Enumerable.Empty<TripModel>();
            }
        }
    }
}