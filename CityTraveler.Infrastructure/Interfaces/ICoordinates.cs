using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface ICoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
