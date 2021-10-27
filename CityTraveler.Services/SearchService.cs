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

        //assuming that if user didn`t give priceLess, priceMore, raitingMore, raitingLess, default values will be given
        //(priceMore = 0, priceLess = double.MaxValue, raitingMore = 0, raitingLess = double.MaxValue)
        public IEnumerable<EntertaimentModel> FilterEntertainments(FilterEntertainment filter)
        {
            EntertainmentService es = new EntertainmentService(_dbContext);
            TripService ts = new TripService(_dbContext);
            try
            {
                if (filter.Type != null)
                {
                    return _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title ?? "")
                            && x.Address.Street.Title.Contains(filter.StreetName ?? "")
                            && x.Address.HouseNumber.Contains(filter.HouseNumber ?? "")
                            && x.Type.Id == filter.Type
                            //need additional methods in services 
                            //&& x.Trips.Contains(ts.getTripsByName(filter.TripName))
                            //&& es.getAveragePrice(x.Prices) > filter.PriceMore
                            //&& es.getAveragePrice(x.Prices) < filter.PriceLess
                            //&& es.getAverageRaiting(x) > filter.RatingMore
                            //&& es.getAverageRaiting(x) < filter.RatingLess
                            );
                }
                else
                {
                    return _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(filter.Title ?? "")
                            && x.Address.Street.Title.Contains(filter.StreetName ?? "")
                            && x.Address.HouseNumber.Contains(filter.HouseNumber ?? "")
                            //need additional methods in services 
                            //&& x.Trips.Contains(ts.getTripsByName(filter.TripName))
                            //&& es.getAveragePrice(x.Prices) > filter.PriceMore
                            //&& es.getAveragePrice(x.Prices) < filter.PriceLess
                            //&& es.getAverageRaiting(x) > filter.RatingMore
                            //&& es.getAverageRaiting(x) < filter.RatingLess
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
            EntertainmentService es = _svContext.EntertainmentService;
            TripService ts = _svContext.TripService;
            UserManagementService us = _svContext.UserManagementService;

            try
            {
                IEnumerable<UserProfileModel> users = us.GetUsersByName(filter.User);
                IEnumerable<EntertaimentModel> enter = null; //es.GetEntartainmentByName(filter.Entertainment)
                if (filter.TripStatus != null)
                {
                    return _dbContext.Trips.Where(x =>
                        x.Description.Contains(filter.Description ?? "")
                        && x.TripEnd == filter.TripEnd
                        && x.TripStart == filter.TripStart
                        && x.RealSpent == filter.RealSpent
                        && x.OptimalSpent == filter.OptimalSpent
                        && x.TripStatus.Id == filter.TripStatus
                        && x.Users.Where(x => users.Contains(x.Profile)) != null
                        && x.Entertaiment.Where(x => enter.Contains(x)) != null
                        && x.Title.Contains(filter.Title ?? "")
                        //need additional methods in services
                        //&& es.getAveragePrice(x.Prices) > filter.PriceMore
                        //&& es.getAveragePrice(x.Prices) < filter.PriceLess
                        //&& es.getAverageRaiting(x) > filter.RatingMore
                        //&& es.getAverageRaiting(x) < filter.RatingLess
                        );
                }
                else 
                {
                    return _dbContext.Trips.Where(x =>
                       x.Description.Contains(filter.Description ?? "")
                       && x.TripEnd == filter.TripEnd
                       && x.TripStart == filter.TripStart
                       && x.RealSpent == filter.RealSpent
                       && x.OptimalSpent == filter.OptimalSpent
                       && x.Users.Where(x => users.Contains(x.Profile)) != null
                       && x.Entertaiment.Where(x => enter.Contains(x)) != null
                       && x.Title.Contains(filter.Title ?? ""));
                        //need additional methods in services
                        //&& es.getAveragePrice(x.Prices) > filter.PriceMore
                        //&& es.getAveragePrice(x.Prices) < filter.PriceLess
                        //&& es.getAverageRaiting(x) > filter.RatingMore
                        //&& es.getAverageRaiting(x) < filter.RatingLess
                }
            }
            catch (Exception e)
            {
                throw new SearchServiceException("Couldn`t filter trips");
                //return null;
            }
        }

        public IEnumerable<ApplicationUserModel> FilterUsers(FilterUsers filter)
        {
            TripService ts = _svContext.TripService;
            EntertainmentService es = _svContext.EntertainmentService;
            try
            {
                IEnumerable<TripModel> trips = null; // ts.getTripsByName(filter.TripName)
                IEnumerable<EntertaimentModel> entertaiments = null; //es.getEntertainmentsByName(filter.eintertainment)
                IEnumerable<TripModel> tripsWithGivenEntrtainments = _dbContext.Trips.Where(x => x.Entertaiment.Where(y => entertaiments.Contains(y)) != null);
                return _dbContext.Users.Where(x =>
                    x.UserName.Contains(filter.UserName ?? "")
                    && x.Profile.Gender.Contains(filter.Gender ?? "")
                    && x.Trips.Where(x=>trips.Contains(x))!=null
                    && x.Trips.Where(x=> tripsWithGivenEntrtainments.Contains(x))!=null
                    );
            }
            catch (Exception e)
            {
                throw new SearchServiceException("Couldn`t filter users");
                //return null;
            }
        }

        public IEnumerable<ApplicationUserModel> FilterUsersAlike(UserProfileModel u)
        {
            try
            {
                return _dbContext.Users.Where(x => x.Profile.Gender == u.Gender 
                && x.Profile.Name.Contains(u.Name));
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
                    return _dbContext.Trips.Where(x =>
                        x.Description.Contains(t.Description ?? "")
                        && x.TripEnd == t.TripEnd
                        && x.TripStart == t.TripStart
                        && x.RealSpent == t.RealSpent
                        && x.OptimalSpent == t.OptimalSpent
                        && x.TripStatus == t.TripStatus
                        );
            }
            catch (Exception e)
            {
                throw new SearchServiceException("Couldn`t get alike trips");
               // return null;
            }
        }

        public IEnumerable<EntertaimentModel> FilterEntertainmentsAlike(EntertaimentModel e)
        {
            try
            {
                return _dbContext.Entertaiments.Where(x =>
                               x.Title.Contains(e.Title ?? "")
                            && x.Address.Street.Title.Contains(e.Address.Street.Title ?? "")
                            && x.Address.HouseNumber.Contains(e.Address.HouseNumber ?? "")
                            && x.Type == e.Type
                            );
            } catch (Exception)
            {
                throw new SearchServiceException("Couldn`t get alike entertainmens");
                //return null;
            }
        }
    }
}
