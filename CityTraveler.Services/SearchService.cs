using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Errors;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
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
        private readonly ServiceContext _svContext;

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public SearchService(ApplicationContext dbContext, ServiceContext sc)
        {
            _dbContext = dbContext;
            _svContext = sc;
        }
        public IEnumerable<EntertaimentModel> FilterEntertainments(FilterEntertainment filter)
        {
            if (filter.PriceLess < filter.PriceMore || filter.RatingLess < filter.RatingMore) 
            {
                throw new SearchServiceException("PriceMore can`t be more than priceLess. The same is for rating.");
            }
            IEnumerable<TripModel> trips = _svContext.TripService.GetTripsByName(filter.TripName ?? "");
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
                            && x.AverageRating < filter.RatingLess
                            );
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
                            && x.AverageRating < filter.RatingLess
                            );
                }
            }
            catch (Exception e) 
            {
                throw new SearchServiceException("Couldn`t filter entertainments");
               // return null;
            }
        }

        public IEnumerable<TripModel> FilterTrips(FilterTrips filter)
        {
            if (filter.PriceLess < filter.PriceMore || filter.AverageRatingLess < filter.AverageRatingMore)
            {
                throw new SearchServiceException("PriceMore can`t be more than priceLess. The same is for rating.");
            }
            EntertainmentService es = _svContext.EntertainmentService;
            TripService ts = _svContext.TripService;
            UserManagementService us = _svContext.UserManagementService;

            try
            {
                IEnumerable<ApplicationUserModel> users = GetUsersByName(filter.User ?? "");
                //IEnumerable<EntertaimentModel> enter = null; //es.GetEntartainmentByName(filter.Entertainment)
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
                       // && x.Entertaiment.Where(x => enter.Contains(x)) != null
                        && x.Title.Contains(filter.Title ?? "")
                        && x.Price.Value > filter.PriceMore
                        && x.Price.Value < filter.PriceLess
                        && x.AverageRating > filter.AverageRatingMore
                        && x.AverageRating < filter.AverageRatingLess
                        );
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
                       //&& x.Entertaiment.Where(x => enter.Contains(x)) != null
                       && x.Title.Contains(filter.Title ?? "")
                       && x.Price.Value > filter.PriceMore
                       && x.Price.Value < filter.PriceLess
                       && x.AverageRating > filter.AverageRatingMore
                       && x.AverageRating < filter.AverageRatingLess);
                }
            }
            catch (Exception e)
            {
                throw new SearchServiceException("Couldn`t filter trips");
                //return null;
            }
        }

        public async Task<IEnumerable<ApplicationUserModel>> FilterUsers(FilterUsers filter)
        {
            TripService ts = _svContext.TripService;
            EntertainmentService es = _svContext.EntertainmentService;
            try
            {
                //IEnumerable<TripModel> trips = _svContext.TripService.GetTripsByName(filter.);
                //IEnumerable<EntertaimentModel> entertaiments = await _svContext.EntertainmentService.GetEntertainmentByTitle(filter.EntertainmentName);
                //IEnumerable<TripModel> tripsWithGivenEntrtainments = _dbContext.Trips.Where(
                   // x => x.Entertaiment.Where(y => entertaiments.Contains(y)) != null);
                return _dbContext.Users.Where(x =>
                    x.UserName.Contains(filter.UserName ?? "")
                    && x.Profile.Gender.Contains(filter.Gender ?? "")
                   // && x.Trips.Where(x=>trips.Contains(x))!=null
                    //&& x.Trips.Where(x=> tripsWithGivenEntrtainments.Contains(x))!=null
                    );
            }
            catch (Exception e)
            {
                throw new SearchServiceException("Couldn`t filter users");
            }
        }

        public IEnumerable<ApplicationUserModel> FilterUsersAlike(UserProfileModel u)
        {
            try
            {
                return _dbContext.Users.Where(
                    x => x.Profile.Gender.Contains( u.Gender??"" )
                && x.Profile.Name.Contains(u.Name ?? ""));
            }
            catch (Exception e)
            {
                throw new SearchServiceException("Couldn`t get alike users");
                //return null;
            }
        }

        public IEnumerable<TripModel> FilterTripsAlike(TripModel t)
        {
            try
            {
                if (t.TripStatus!=null)
                    return _dbContext.Trips.Where(x =>
                        x.Description.Contains(t.Description ?? "")
                        && x.TripEnd == t.TripEnd
                        && x.TripStart == t.TripStart
                        && x.RealSpent == t.RealSpent
                        && x.OptimalSpent == t.OptimalSpent
                        && x.TripStatus == t.TripStatus
                        );
                else
                    return _dbContext.Trips.Where(x =>
                      x.Description.Contains(t.Description ?? "")
                      && x.TripEnd == t.TripEnd
                      && x.TripStart == t.TripStart
                      && x.RealSpent == t.RealSpent
                      && x.OptimalSpent == t.OptimalSpent);
            }
            catch (Exception e)
            {
                throw new SearchServiceException("Couldn`t get alike trips");
            }
        }

        public IEnumerable<EntertaimentModel> FilterEntertainmentsAlike(EntertaimentModel e)
        {
            try
            {
                if (e.Type != null && e.Address != null)
                {
                    return _dbContext.Entertaiments.Where(x =>
                                   x.Title.Contains(e.Title ?? "")
                                && x.Address.Street.Title.Contains(e.Address.Street.Title ?? "")
                                && x.Address.HouseNumber.Contains(e.Address.HouseNumber ?? "")
                                && x.Type == e.Type
                                && x.Description.Contains(e.Description ?? ""));
                }
                else if (e.Type == null && e.Address == null)
                {
                    return _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(e.Title ?? ""));
                }
                else if (e.Type != null && e.Address == null)
                {
                    return _dbContext.Entertaiments.Where(x =>
                                   x.Title.Contains(e.Title ?? "")
                                && x.Type == e.Type
                                && x.Description.Contains(e.Description ?? ""));
                }
                else
                {
                    return _dbContext.Entertaiments.Where(x =>
                                   x.Title.Contains(e.Title ?? "")
                                && x.Address.Street.Title.Contains(e.Address.Street.Title ?? "")
                                && x.Address.HouseNumber.Contains(e.Address.HouseNumber ?? "")
                                && x.Description.Contains(e.Description ?? ""));
                }

            } catch (Exception ex)
            {
                throw new SearchServiceException($"Couldn`t get alike entertainmens {ex.Message} ");
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
