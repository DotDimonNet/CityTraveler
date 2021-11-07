using CityTraveler.Domain.Entities;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Domain.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Filters.Admin;
using AutoMapper;
using Microsoft.Extensions.Logging;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminPanelService> _logger;
        public bool IsActive { get; set; }
        public string Version { get; set; }
        public AdminPanelService(ApplicationContext context, IMapper mapper, ILogger<AdminPanelService> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<UserChangeAdminDTO>> FilterUsers(FilterAdminUser filter)
        {
            try
            {
                var users = _context.Users.Where(x =>
                            x.UserName.Contains(filter.UserName)
                            && x.Profile.Gender.Contains(filter.Gender)
                            && x.Profile.Name.Contains(filter.Name)
                            && x.Email.Contains(filter.Email)
                            && x.PhoneNumber.Contains(filter.PhoneNumber));
                return users.Select(x => _mapper.Map<ApplicationUserModel, UserChangeAdminDTO>(x));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter users {e.Message}");
                return Enumerable.Empty<UserChangeAdminDTO>();
            }
        }
        public async Task<IEnumerable<EntertaimentModel>> FilterEntertaiments(FilterAdminEntertaiment filter)
        {
            if (filter.AveragePriceLess > filter.AveragePriceMore)
            {
                _logger.LogWarning("PriceLess can`t be more than priceMore");
                return null;
            }
            if(filter.AverageRatingLess > filter.AverageRatingMore)
            {
                _logger.LogWarning("RatingLess can`t be more than RatingMore");
                return null;
            }
            try
            {
                var entertaiment = _context.Entertaiments.Where(x => x.AverageRating > filter.AverageRatingLess
                            && x.AverageRating < filter.AverageRatingMore
                            && x.AveragePrice.Value < filter.AveragePriceMore
                            && x.AveragePrice.Value > filter.AveragePriceLess
                            && x.Description.Contains(filter.Description)
                            && x.Title.Contains(filter.Title));
                 return  (filter.Type != -1) ?  entertaiment.Where(x => x.Type == (EntertainmentType)filter.Type) : entertaiment;

            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter entertainments {e.Message}");
                return null;
            }
        }
        public async Task<IEnumerable<TripDTO>> FilterTrips(FilterAdminTrip filter)
        {
            if (filter.PriceLess < filter.PriceMore)
            {
                _logger.LogWarning("PriceLess can`t be more than priceMore");
                return null;
            }
            if(filter.AverageRatingLess < filter.AverageRatingMore)
            {
                _logger.LogWarning("RatingLess can`t be more than RatingMore");
                return null;
            }
            try
            {
                var trips = _context.Trips.Where(x => x.TripStart > filter.TripStart
                        && x.TripEnd < filter.TripEnd
                        && x.OptimalSpent > filter.OptimalSpent
                        && x.RealSpent > filter.RealSpent
                        && x.Description.Contains(filter.Description)
                        && x.Title.Contains(filter.Title)
                        && x.Price.Value > filter.PriceMore
                        && x.Price.Value < filter.PriceLess
                        && x.AverageRating > filter.AverageRatingMore
                        && x.AverageRating < filter.AverageRatingLess);
                trips = filter.TripStatus != -1 ? trips.Where(x => x.TripStatus.Id == filter.TripStatus) : trips;

                return trips.Select(x => _mapper.Map<TripModel, TripDTO>(x));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter trips {e.Message}");
                return Enumerable.Empty<TripDTO>(); ;
            }
        }
        public async Task<IEnumerable<AddressShowDTO>> FindAdressStreets(FilterAdminStreet filter)
        {
            try
            {
                var address =  _context.Streets.Where(x =>
                               x.Description.Contains(filter.Description)
                               && x.Title.Contains(filter.Title)).SelectMany(x => x.Addresses);
                return address.Select(x => _mapper.Map<AddressModel, AddressShowDTO>(x));

            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter steets {e.Message}");
                return Enumerable.Empty<AddressShowDTO>();
            }
        }
        public async Task<IEnumerable<ReviewDTO>> FilterReview(FilterAdminReview filter)
        {
            if (filter.RatingLess < filter.RatingMore)
            {
                _logger.LogWarning("RatingMore can`t be more than RatingLess.");
                return null;
            }
            try
            {
                var rates = _context.Reviews.Where(x =>
                        x.Rating.Value > filter.RatingLess
                        && x.Rating.Value < filter.RatingMore
                        && x.Description.Contains(filter.Description)
                        && x.User.UserName.Contains(filter.User)
                        && x.Title.Contains(filter.Title));
                return rates.Select(x => _mapper.Map<ReviewModel, ReviewDTO>(x));
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Failed to filter reviews {e.Message}");
                return Enumerable.Empty<ReviewDTO>();
            }
        }
    }
}
