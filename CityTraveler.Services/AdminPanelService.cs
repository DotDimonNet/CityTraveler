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
using CityTraveler.Domain.Filters.Admin;

namespace CityTraveler.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly ApplicationContext _context;
        private readonly ServiceContext _svContext;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public AdminPanelService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ApplicationUserModel>> AdminFilterUsers(FilterAdminUser filter)
        {
            try
            {
                var users =  _context.Users.Where(x =>
                    x.UserName.Contains(filter.UserName ?? "")
                    && x.Profile.Gender.Contains(filter.Gender ?? "")
                    && x.Profile.Name.Contains(filter.Name ?? "")
                    && x.Email.Contains(filter.Email ?? "")
                    && x.PhoneNumber.Contains(filter.PhoneNumber ?? ""));
                    
                return users;
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn`t filter users, {e.Message}");
            }
        }
        public async Task<IEnumerable<EntertaimentModel>> AdminFilterEntertaiments(FilterAdminEntertaiment filter)
        {
            try
            {
                var entertaiments = _context.Entertaiments.Where(x => x.AverageRating > filter.AverageRatingLess
                && x.AverageRating < filter.AverageRatingMore
                && x.AveragePrice.Value < filter.AveragePriceMore
                && x.AveragePrice.Value > filter.AveragePriceLess
                && x.Description.Contains(filter.Description ?? " ")
                && x.Type.Id == filter.Type
                && x.Title.Contains(filter.Title ?? " "));

                return entertaiments;
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn`t filter trip, {e.Message}");
            }
        }
        public async Task<IEnumerable<TripModel>> AdminFilterTrips(FilterAdminTrip filter)
        {
            try
            {
                var trips = _context.Trips.Where(x => x.TripStart > filter.TripStart
                && x.TripEnd < filter.TripEnd
                && x.OptimalSpent > filter.OptimalSpent
                && x.RealSpent > filter.RealSpent
                && x.Description.Contains(filter.Description ?? " ")
                && x.Title.Contains(filter.Title ?? " "));


                return trips;
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn`t filter trip, {e.Message}");
            }
        }
        public async Task<IEnumerable<AddressModel>> AdminFilterStreets(FilterAdminStreet filter)
        {
            try
            {
                var addressList = _context.Streets.Where(x => 
                x.Description.Contains(filter.Description ?? " ")
                && x.Title.Contains(filter.Title ?? " ")).SelectMany(x => x.Addresses);

                return addressList;
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn`t filter trip, {e.Message}");
            }
        }
        public async Task<IEnumerable<ReviewModel>> AdminFilterReview(FilterAdminReview filter)
        {
            try
            {
                var reviews = _context.Reviews.Where(x => x.Rating.Value > filter.RatingLess
                && x.Rating.Value < filter.RatingMore
                && x.Description.Contains(filter.Description ?? " ")
                && x.User.UserName.Contains(filter.User ?? " ")
                && x.Title.Contains(filter.Title ?? " "));

                return reviews;
            }
            catch (Exception e)
            {
                throw new Exception($"Couldn`t filter trip, {e.Message}");
            }
        }
    }
}
