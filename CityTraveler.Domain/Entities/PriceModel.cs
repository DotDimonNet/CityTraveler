using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class PriceModel : Entity
    {
        public string Title { get; set; }
        public double Value { get; set; }
    }

    public class TripPriceModel: PriceModel
    {
        public virtual Guid TripId { get; set; }
        public virtual TripModel Trip { get; set; }
    }

    public class EntertaimentPriceModel : PriceModel
    {
        public virtual Guid EntertaimentId { get; set; }
        public virtual EntertaimentModel Entertaiment { get; set; }
    }
}
