using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IImage : IDbData, IDescribable
    {
        public string Source { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
