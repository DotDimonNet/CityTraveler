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
        public virtual TripModel Trip { get; set; }
    }

    public class InstitutionPriceModel : PriceModel
    {
        public virtual InstitutionModel Institution { get; set; }
    }
}
