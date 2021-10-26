using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class FilterEntertainment
    {
        //assuming that if user didn`t give priceLess, priceMore, raitingMore, raitingLess, default values will be given
        //(priceMore = 0, priceLess = double.MaxValue, raitingMore = 0, raitingLess = double.MaxValue)
        public string StreetName { get; set; }
        public int Type { get; set; }
        public string HouseNumber { get; set; }
        public string TripName { get; set; }
        public string Title { get; set; }
        public double PriceMore { get; set; }
        public double PriceLess { get; set; }
        //public string Description { get; set; }
        public double RatingMore { get; set; }
        public double RatingLess { get; set; }
    }
}
