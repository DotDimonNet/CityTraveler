using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IPrice : IDbData
    {
        public string Title { get; set; }
        public double Value { get; set; }
    }
}
