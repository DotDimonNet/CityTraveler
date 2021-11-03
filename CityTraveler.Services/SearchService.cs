using AutoMapper;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.DTO;
using CityTraveler.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class SearchService : ISearchService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<SearchService> _logger;
        private readonly IEntertainmentService _entertainmentService;
        public bool IsActive { get; set; }
        public string Version { get; set; }

        public SearchService(ApplicationContext dbContext, IMapper mapper, ILogger<SearchService> logger, IEntertainmentService entertainmentService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _entertainmentService = entertainmentService;
        }
        public IEnumerable<EntertaimentModel> FilterEntertainments(FilterEntertainment filter)
        {
            if (filter.PriceLess < filter.PriceMore || filter.RatingLess < filter.RatingMore) 
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess. The same is for rating.");
                return null;
            }
            
            IEnumerable<TripModel> trips = GetTripByName(filter.TripName ?? "");
            
            try
            {
                if (filter.Type != -1)
                {
                    return _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title ?? "")
                            && x.Address.Street.Title.Contains(filter.StreetName ?? "")
                            && x.Address.HouseNumber.Contains(filter.HouseNumber ?? "")
                            && x.Type.Id == filter.Type
                            && x.Trips.Where(x=>trips.Contains(x)).Count() != 0
                            && x.AveragePrice.Value > filter.PriceMore
                            && x.AveragePrice.Value < filter.PriceLess
                            && x.AverageRating > filter.RatingMore
                            && x.AverageRating < filter.RatingLess);
                }
                else
                {
                    return _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title ?? "")
                            && x.Address.Street.Title.Contains(filter.StreetName ?? "")
                            && x.Address.HouseNumber.Contains(filter.HouseNumber ?? "")
                            && x.Trips.Where(x => trips.Contains(x)).Count() != 0
                            && x.AveragePrice.Value > filter.PriceMore
                            && x.AveragePrice.Value < filter.PriceLess
                            && x.AverageRating > filter.RatingMore
                            && x.AverageRating < filter.RatingLess);
                }
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to filter entertainments {e.Message}");
                return null;
            }
        }

        public IEnumerable<TripModel> FilterTrips(FilterTrips filter)
        {
            if (filter.PriceLess < filter.PriceMore || filter.AverageRatingLess < filter.AverageRatingMore)
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess. The same is for rating.");
                return null;
            }

            try
            {
                IEnumerable<ApplicationUserModel> users = GetUsersByName(filter.User ?? "");
                IEnumerable<EntertaimentModel> enter =  _entertainmentService.GetEntertainmentsByTitle(filter.EntertaimentName ?? "");
                if (filter.TripStatus != -1)
                {
                    return _dbContext.Trips.Where(x =>
                        x.Description.Contains(filter.Description ?? "")
                        && x.TripEnd > filter.TripEnd
                        && x.TripStart > filter.TripStart
                        && x.RealSpent > filter.RealSpent
                        && x.OptimalSpent > filter.OptimalSpent
                        && x.TripStatus.Id == filter.TripStatus
                        && x.Users.Where(x => users.Contains(x)).Count() != 0
                        && x.Entertaiment.Where(x => enter.Contains(x)).Any()
                        && x.Title.Contains(filter.Title ?? "")
                        && x.Price.Value > filter.PriceMore
                        && x.Price.Value < filter.PriceLess
                        && x.AverageRating > filter.AverageRatingMore
                        && x.AverageRating < filter.AverageRatingLess);
                }
                else 
                {
                    return _dbContext.Trips.Where(x =>
                       x.Description.Contains(filter.Description ?? "")
                       && x.TripEnd > filter.TripEnd
                       && x.TripStart > filter.TripStart
                       && x.RealSpent > filter.RealSpent
                       && x.OptimalSpent > filter.OptimalSpent
                       && x.Users.Where(x => users.Contains(x)).Count()!=-1
                       && x.Entertaiment.Where(x => enter.Contains(x)).Any()
                       && x.Title.Contains(filter.Title ?? "")
                       && x.Price.Value > filter.PriceMore
                       && x.Price.Value < filter.PriceLess
                       && x.AverageRating > filter.AverageRatingMore
                       && x.AverageRating < filter.AverageRatingLess);
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter trips {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ApplicationUserModel>> FilterUsers(FilterUsers filter)
        {
            try
            {
                IEnumerable<EntertaimentModel> entertaiments =  _entertainmentService.GetEntertainmentsByTitle(filter.EntertainmentName);
                IEnumerable<TripModel> tripsWithGivenEntrtainments = _dbContext.Trips.Where(
                x => x.Entertaiment.Where(y => entertaiments.Contains(y)).Any());
                return _dbContext.Users.Where(x =>
                    x.UserName.Contains(filter.UserName ?? "")
                    && x.Profile.Gender.Contains(filter.Gender ?? "")
                    && x.Trips.Where(x=> tripsWithGivenEntrtainments.Contains(x)).Any());
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter users {e.Message}");
                return null;
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
