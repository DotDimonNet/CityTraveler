using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IReview : IDbData, IDescribable
    {
        public IRating Rating { get; set; }
        public IEnumerable<IImage> Images { get; set; }
    }
}
