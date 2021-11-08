using AutoMapper;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.Enums;
using CityTraveler.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CityTraveler.Services
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationContext _dbContext;
        private readonly ILogger<SearchService> _logger;
        private readonly IEntertainmentService _entertainmentService;
        public bool IsActive { get; set; }
        public string Version { get; set; }

        public SearchService(ApplicationContext dbContext, ILogger<SearchService> logger, IEntertainmentService entertainmentService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _entertainmentService = entertainmentService;
        }

        public async Task<IEnumerable<EntertaimentModel>> FilterEntertainments(FilterEntertainment filter)
        {
            if (filter.PriceLess < filter.PriceMore) 
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess."); 
                return Enumerable.Empty<EntertaimentModel>();
            }
            if (filter.RatingLess < filter.RatingMore)
            {
                _logger.LogWarning("RatingMore can`t be more than ratingLess.");
                return Enumerable.Empty<EntertaimentModel>();
            }
            var trips = GetTripByName(filter.TripName);
            
            try
            {
                return filter.Type != -1 ? 
                    await Task.Run(() => _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title)
                            && x.Address.Street.Title.Contains(filter.StreetName)
                            && x.Address.HouseNumber.Contains(filter.HouseNumber)
                            && x.Type == (EntertainmentType)filter.Type
                            && x.Trips.Where(x=>trips.Contains(x)).Any()
                            && x.AveragePrice.Value >= filter.PriceMore
                            && x.AveragePrice.Value <= filter.PriceLess
                            && x.AverageRating >= filter.RatingMore
                            && x.AverageRating <= filter.RatingLess)) :
                    await Task.Run(() => _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title)
                            && x.Address.Street.Title.Contains(filter.StreetName)
                            && x.Address.HouseNumber.Contains(filter.HouseNumber)
                            && x.Trips.Where(x => trips.Contains(x)).Any()
                            && x.AveragePrice.Value >= filter.PriceMore
                            && x.AveragePrice.Value <= filter.PriceLess
                            && x.AverageRating >= filter.RatingMore
                            && x.AverageRating <= filter.RatingLess));
            }
            catch (Exception e) 
            {
                _logger.LogError($"Failed to filter entertainments {e.Message}");
                return Enumerable.Empty<EntertaimentModel>();
            }
        }

        public async Task<IEnumerable<TripModel>> FilterTrips(FilterTrips filter)
        {
            if (filter.PriceLess < filter.PriceMore)
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess. ");
                return Enumerable.Empty<TripModel>();
            }
            if (filter.AverageRatingLess < filter.AverageRatingMore) 
            {
                _logger.LogWarning("RatingMore can`t be more than ratingLess.");
                return Enumerable.Empty<TripModel>();
            }

            try
             {
                var users = GetUsersByName(filter.User ?? "");
                var entertainment =  _entertainmentService.GetEntertainmentsByTitle(filter.EntertaimentName);
                return await Task.Run(() => filter.TripStatus != -1 ?
                        _dbContext.Trips.Where(x =>
                        x.Description.Contains(filter.Description)
                        && x.TripEnd >= filter.TripEnd 
                        && x.TripStart >= filter.TripStart
                        && x.RealSpent >= filter.RealSpent
                        && x.OptimalSpent >= filter.OptimalSpent
                        && x.TripStatus.Id == filter.TripStatus
                        && x.Users.Where(x => users.Contains(x)).Any()
                        && x.Entertaiments.Where(x => entertainment.Contains(x)).Any()
                        && x.Title.Contains(filter.Title)
                        && x.Price.Value >= filter.PriceMore
                        && x.Price.Value <= filter.PriceLess
                        && x.AverageRating >= filter.AverageRatingMore
                        && x.AverageRating <= filter.AverageRatingLess)
                    :  _dbContext.Trips.Where(x =>
                       x.Description.Contains(filter.Description)
                       && x.TripEnd >= filter.TripEnd
                       && x.TripStart >= filter.TripStart
                       && x.RealSpent >= filter.RealSpent
                       && x.OptimalSpent >= filter.OptimalSpent
                       && x.Users.Where(x => users.Contains(x)).Count()!=-1
                       && x.Entertaiments.Where(x => entertainment.Contains(x)).Any()
                       && x.Title.Contains(filter.Title)
                       && x.Price.Value >= filter.PriceMore
                       && x.Price.Value <= filter.PriceLess
                       && x.AverageRating >= filter.AverageRatingMore
                       && x.AverageRating <= filter.AverageRatingLess));

            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to filter trips {e.Message}");
                return Enumerable.Empty<TripModel>();
            }
        }

        public async Task<IEnumerable<ApplicationUserModel>> FilterUsers(FilterUsers filter)
        {
            try
            {
                var entertaiments =  _entertainmentService.GetEntertainmentsByTitle(filter.EntertainmentName);
                var entertainmentsIds = entertaiments.Select(x => x.Id);
                var trips = _dbContext.Entertaiments
                    .Where(x => entertainmentsIds.Contains(x.Id))
                    .SelectMany(x => x.Trips);
                return await Task.Run(() => trips.Any() 
                    ? _dbContext.Users.Where(x =>
                        x.UserName.Contains(filter.UserName)
                        && x.Profile.Gender.Contains(filter.Gender)
                        && x.Trips.Where(x => trips.Contains(x)).Any())
                    : Enumerable.Empty<ApplicationUserModel>());
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter users {e.Message}");
                return Enumerable.Empty<ApplicationUserModel>();
            }
        }

        public IEnumerable<ApplicationUserModel> GetUsersByName(string name = "")
        {
            return _dbContext.Users.Where(x => x.Profile.Name.Contains(name));
        }

        public IEnumerable<TripModel> GetTripByName(string name = "")
        {
            return _dbContext.Trips.Where(x => x.Title.Contains(name));
        }
    }
}
