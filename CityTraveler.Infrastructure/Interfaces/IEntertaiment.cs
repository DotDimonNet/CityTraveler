using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IEntertaiment : IDbData, IDescribable
    {
        public IEnumerable<IImage> Images { get; set; }
        public IEnumerable<IReview> Reviews { get; set; }
        public IAddress Address { get; set; }
        public Dictionary<DayOfWeek, DateTime> Open { get; set; }
        public Dictionary<DayOfWeek, DateTime> Closed { get; set; }
        public IEnumerable<IPrice> Prices { get; set; }
    }
}
