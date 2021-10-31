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
        public string StreetName { get; set; }
        public int Type { get; set; } = -1;
        public string HouseNumber { get; set; }
        public string TripName { get; set; }
        public string Title { get; set; }
        public double PriceMore { get; set; } = 0;
        public double PriceLess { get; set; } = double.MaxValue;
        //public string Description { get; set; }
        public double RatingMore { get; set; } = 0;
        public double RatingLess { get; set; } = 5;
    }
}
