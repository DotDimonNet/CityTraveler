using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CityTraveler.Domain.Enums;

namespace CityTraveler.Domain.Entities
{
    public class InstitutionModel : Entertaiment
    {
        //public Dictionary<DayOfWeek, DateTime> Open { get; set; } = new Dictionary<DayOfWeek, DateTime>();
        //public Dictionary<DayOfWeek, DateTime> Closed { get; set; } = new Dictionary<DayOfWeek, DateTime>();
        public virtual ICollection<InstitutionPriceModel> Prices { get; set; } = new List<InstitutionPriceModel>();

        public virtual ICollection<TripModel> Trips { get; set; } = new List<TripModel>();
        public virtual InstitutionType Type { get; set; }
    }
}
