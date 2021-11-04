using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Filters.Admin
{
    public class FilterAdminTrip
    {
        public DateTime TripStart { get; set; } = DateTime.MinValue;
        public DateTime TripEnd { get; set; } = DateTime.MinValue;
        public double PriceMore { get; set; } = 0;
        public double PriceLess { get; set; } = double.MaxValue;
        public double AverageRatingMore { get; set; } = 0;
        public double AverageRatingLess { get; set; } = 5;
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan OptimalSpent { get; set; } = TimeSpan.MinValue;
        public TimeSpan RealSpent { get; set; } = TimeSpan.MinValue;
        public int TripStatus { get; set; } = -1;
    }
}
