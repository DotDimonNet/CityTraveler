using CityTraveler.Domain.Entities;
using CityTraveler.Repository.DbContext;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CityTraveler.Domain.Enums;
namespace CityTraveler.Services
{
    public class StatisticService : IStatisticService
    {
        // 
        private ApplicationContext _context;
        public StatisticService(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<int> QuantityPassEntertaiment(Guid UserID)
        {
            int result;
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserID);
                result = user.Trips.SelectMany(x => x.Entertaiment).Distinct().Count();
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
            return result;
        }
        public async Task<IEnumerable<TripModel>> GetTripVisitEntertaiment(Guid UserID,EntertaimentModel rev)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserID);
                return user.Trips.Where(x => x.Entertaiment.Contains(rev));
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
             
        }
        public async Task<double> GetAverageRatingUserTrip(Guid userId)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
                return user.Trips.Average(x => x.AverageRating);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        public async Task<double> GetAverageEntertaimentUserTrip(Guid UserID)
        {
            double result;
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserID);
                result = user.Trips.Select(x => x.Entertaiment.Count).Average();
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
            return result;
        }
        public async Task<TimeSpan> GetAverageTimeUserTrip(Guid UserID)
        {
            double result;
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserID);
                result = user.Trips.Select(x => x.TripEnd.Subtract(x.TripStart)).Average(t => t.Ticks);
                long averageTicksLong = Convert.ToInt64(result);

                TimeSpan averageTimeSpan = TimeSpan.FromTicks(averageTicksLong);

                return averageTimeSpan;
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
        }
        public async Task<double> GetAveragePriceUserTrip(Guid UserID)
        {
            double result;
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserID);
                result = user.Trips.Select(x => x.Price.Value).Average();
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
            return result;
        }
        public async Task<int> GetCountPassedUserTrip(Guid UserID)
        {
            
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserID);
                
                return user.Trips.Count(x => x.TripStatus == TripStatus.Passed);
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
            
        }
        public async Task<IEnumerable<TripModel>> GetActivityUserTrip(Guid UserID,DateTime time)
        {
             
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == UserID);
                 return user.Trips.Where(x => x.Created < time);
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
          
        }
        public async Task<double> GetAverageAgeUser()
        {
            try
            {
                return _context.UserProfiles.Select(x =>
                     DateTime.Today.Year - x.Birthday.Year
                ).Average();
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
        }
        public async Task<double> GetAvarageEnternaimentInTrip()
        {
            
            try
            {
               return _context.Trips.Select(x => x.Entertaiment.Count()).Average();
            }
            catch (Exception e)
            {
                throw new Exception(" ");
            }
            
        }
    }

}
