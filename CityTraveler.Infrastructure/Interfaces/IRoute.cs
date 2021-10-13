using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IRoute : IDbData
    {
        public IEnumerable<ICoordinates> Coordinates { get; set; }
        public TimeSpan OptimalSpent { get; set; }
        public TimeSpan RealSpent { get; set; }
        public IEnumerable<IPrice> Prices { get; set; }
    }
}
