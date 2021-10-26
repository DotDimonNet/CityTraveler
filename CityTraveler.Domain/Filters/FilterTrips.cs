using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class FilterTrips
    {
        //assuming that if user didn`t give priceLess, priceMore, raitingMore, raitingLess, default values will be given
        //(priceMore = 0, priceLess = double.MaxValue, raitingMore = 0, raitingLess = double.MaxValue)
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public string EntertaimentName { get; set; }
        public string User{ get; set; }
        public double PriceMore { get; set; }
        public double PriceLess { get; set; }
        public double AverageRatingMore { get; set; }
        public double AverageRatingLess { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan OptimalSpent { get; set; }
        public TimeSpan RealSpent { get; set; }
        public int TripStatus { get; set; }
    }
}
