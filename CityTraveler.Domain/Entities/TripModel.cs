using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CityTraveler.Domain.Enums;

namespace CityTraveler.Domain.Entities
{
    public class TripModel : Entity, IDescribable
    {
        public DateTime TripStart { get; set; }
        public DateTime TripEnd { get; set; }
        public virtual ICollection<EntertaimentModel> Entertaiments { get; set; } = new List<EntertaimentModel>();
        public virtual ICollection<ApplicationUserModel> Users { get; set; } = new List<ApplicationUserModel>();
        public virtual ICollection<TripImageModel> Images { get; set; } = new List<TripImageModel>();
        public virtual ICollection<TripReviewModel> Reviews { get; set; } = new List<TripReviewModel>();
        public Guid TemplateId { get; set; } 
        public virtual TripPriceModel Price { get; set; }
        public double AverageRating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan OptimalSpent { get; set; }
        public TimeSpan RealSpent { get; set; }
        public virtual TripStatus TripStatus { get; set; }
        public bool DafaultTrip { get; set; } 
        public string TagString { get; set; }
    }
}
