using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IAddress : IDescribable, IDbData
    {
        public ICoordinates Coordinates { get; set; }
        public IStreet Street { get; set; }
        public string HouseNumber { get; set; }
    }
}
