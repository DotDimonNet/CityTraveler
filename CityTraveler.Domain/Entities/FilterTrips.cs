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
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public virtual ICollection<EntertaimentModel> Entertaiment { get; set; } = new List<EntertaimentModel>();
        public virtual ICollection<ApplicationUserModel> Users { get; set; } = new List<ApplicationUserModel>();
        public virtual ICollection<TripReviewModel> Reviews { get; set; } = new List<TripReviewModel>();
        public virtual TripPriceModel Price { get; set; }
        public double AverageRating { get; set; }
        public string Title { get; set; }
        public TimeSpan OptimalSpent { get; set; }
        public TimeSpan RealSpent { get; set; }
        public virtual TripStatus TripStatus { get; set; }
    }
}
