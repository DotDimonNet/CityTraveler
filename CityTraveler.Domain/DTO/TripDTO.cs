using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class TripDTO
    {
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        //public int TripStatus { get; set; }
        public string TagSting { get; set; }
        public IEnumerable<TripImageModel> Images { get; set; } = new List<TripImageModel>();
        public IEnumerable<TripReviewModel> Reviews { get; set; } = new List<TripReviewModel>();
        public IEnumerable<EntertaimentModel> Entertaiments { get; set; } = new List<EntertaimentModel>();
    }
    public class AddNewTripDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TripStart { get; set; }
        public IEnumerable<EntertaimentModel> Entertaiments { get; set; } = new List<EntertaimentModel>();
        //public int TripStatus { get; set; }
    }

    public class DefaultTripDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //public int TripStatus { get; set; }
        public string TagSting { get; set; }
        public TripPriceModel Price { get; set; }
        public double AverageRating { get; set; }
        public TimeSpan OptimalSpent { get; set; }
        public IEnumerable<TripImageModel> Images { get; set; } = new List<TripImageModel>();
        public IEnumerable<TripReviewModel> Reviews { get; set; } = new List<TripReviewModel>();
        public IEnumerable<EntertaimentModel> Entertaiments { get; set; } = new List<EntertaimentModel>();
        public bool DefaultTrip { get; set; }
    }
}
