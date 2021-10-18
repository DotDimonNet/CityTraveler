using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class LandskapeModel : Entertaiment 
    {
        public virtual ICollection<TripModel> Trips { get; set; } = new List<TripModel>();
    }
}
