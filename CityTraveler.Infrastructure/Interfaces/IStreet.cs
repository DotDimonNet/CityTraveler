using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IStreet : IDbData, IDescribable
    {
        public IEnumerable<ICoordinates> Coordinates { get; set; }
    }
}
