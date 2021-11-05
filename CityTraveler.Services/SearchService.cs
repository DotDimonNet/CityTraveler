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
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<EntertaimentModel>> FilterEntertainments(FilterEntertainment filter)
        {
            if (filter.PriceLess < filter.PriceMore || filter.RatingLess < filter.RatingMore) 
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess. The same is for rating.");
                return null;
            }
            
            var trips = GetTripByName(filter.TripName ?? "");
            
            try
            {
                return filter.Type != -1? 
                    await Task.Run(() => _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title ?? "")
                            && x.Address.Street.Title.Contains(filter.StreetName ?? "")
                            && x.Address.HouseNumber.Contains(filter.HouseNumber ?? "")
                            && x.Type.Id == filter.Type
                            && x.Trips.Where(x=>trips.Contains(x)).Any()
                            && x.AveragePrice.Value > filter.PriceMore
                            && x.AveragePrice.Value < filter.PriceLess
                            && x.AverageRating > filter.RatingMore
                            && x.AverageRating < filter.RatingLess)):
                    await Task.Run(() => _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title ?? "")
                            && x.Address.Street.Title.Contains(filter.StreetName ?? "")
                            && x.Address.HouseNumber.Contains(filter.HouseNumber ?? "")
                            && x.Trips.Where(x => trips.Contains(x)).Any()
                            && x.AveragePrice.Value > filter.PriceMore
                            && x.AveragePrice.Value < filter.PriceLess
                            && x.AverageRating > filter.RatingMore
                            && x.AverageRating < filter.RatingLess));
            }
            catch (Exception e) 
            {
                _logger.LogWarning($"Failed to filter entertainments {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<TripModel>> FilterTrips(FilterTrips filter)
        {
            if (filter.PriceLess < filter.PriceMore || filter.AverageRatingLess < filter.AverageRatingMore)
            {
                _logger.LogWarning("PriceMore can`t be more than priceLess. The same is for rating.");
                return null;
            }

            try
             {
                var users = GetUsersByName(filter.User ?? "");
                var entertainment =  _entertainmentService.GetEntertainmentsByTitle(filter.EntertaimentName ?? "");
                return filter.TripStatus != -1 ?
                    await Task.Run(() => _dbContext.Trips.Where(x =>
                        x.Description.Contains(filter.Description ?? "")
                        && x.TripEnd > filter.TripEnd
                        && x.TripStart > filter.TripStart
                        && x.RealSpent > filter.RealSpent
                        && x.OptimalSpent > filter.OptimalSpent
                        && x.TripStatus.Id == filter.TripStatus
                        && x.Users.Where(x => users.Contains(x)).Any()
                        && x.Entertaiment.Where(x => entertainment.Contains(x)).Any()
                        && x.Title.Contains(filter.Title ?? "")
                        && x.Price.Value > filter.PriceMore
                        && x.Price.Value < filter.PriceLess
                        && x.AverageRating > filter.AverageRatingMore
                        && x.AverageRating < filter.AverageRatingLess)):
                    await Task.Run(() => _dbContext.Trips.Where(x =>
                       x.Description.Contains(filter.Description ?? "")
                       && x.TripEnd > filter.TripEnd
                       && x.TripStart > filter.TripStart
                       && x.RealSpent > filter.RealSpent
                       && x.OptimalSpent > filter.OptimalSpent
                       && x.Users.Where(x => users.Contains(x)).Count()!=-1
                       && x.Entertaiment.Where(x => entertainment.Contains(x)).Any()
                       && x.Title.Contains(filter.Title ?? "")
                       && x.Price.Value > filter.PriceMore
                       && x.Price.Value < filter.PriceLess
                       && x.AverageRating > filter.AverageRatingMore
                       && x.AverageRating < filter.AverageRatingLess));

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
                var entertaiments =  _entertainmentService.GetEntertainmentsByTitle(filter.EntertainmentName?? "");
                var trips = new List<TripModel>();
                foreach (var entertainment in entertaiments)
                {
                    var entertaiment = await _dbContext.Entertaiments.FirstOrDefaultAsync(x => x.Id == entertainment.Id);
                    trips.AddRange(entertaiment.Trips);
                }
                return trips.Any()?
                await Task.Run(() => _dbContext.Users.Where(x =>
                        x.UserName.Contains(filter.UserName ?? "")
                        && x.Profile.Gender.Contains(filter.Gender ?? "")
                        && x.Trips.Where(x => trips.Contains(x)).Any())):
                await Task.Run(() => _dbContext.Users.Where(x =>
                        x.UserName.Contains(filter.UserName ?? "")
                        && x.Profile.Gender.Contains(filter.Gender ?? "")));
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
