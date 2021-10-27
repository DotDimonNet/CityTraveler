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
        public virtual ICollection<TripModel> Trips { get; set; } = new List<TripModel>();
        public virtual EntertaimentPriceModel AveragePrice { get; set; }
        public virtual ICollection<EntertaimentImageModel> Images { get; set; } = new List<EntertaimentImageModel>();
        public virtual ICollection<EntertainmentReviewModel> Reviews { get; set; } = new List<EntertainmentReviewModel>();
        public virtual double AverageRating { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //Need to create a json for institution's and event's properties  
    }
}
