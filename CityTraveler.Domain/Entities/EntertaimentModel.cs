using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CityTraveler.Domain.Enums;
    
namespace CityTraveler.Domain.Entities
{
    public class EntertaimentModel : Entity, IDescribable
    {
        public virtual Guid AddressId { get; set; }
        public virtual AddressModel Address { get; set; }
        public virtual EntertainmentType Type { get; set; }
        public virtual IEnumerable<TripModel> Trips { get; set; } = new List<TripModel>();
        public virtual EntertaimentPriceModel AveragePrice { get; set; }
        public virtual IEnumerable<EntertaimentImageModel> Images { get; set; } = new List<EntertaimentImageModel>();
        public virtual IEnumerable<EntertainmentReviewModel> Reviews { get; set; } = new List<EntertainmentReviewModel>();
        public virtual double AverageRating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}
