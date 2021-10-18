using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class EventModel : Entertaiment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual ICollection<TripModel> Trips { get; set; } = new List<TripModel>();
    }
}
