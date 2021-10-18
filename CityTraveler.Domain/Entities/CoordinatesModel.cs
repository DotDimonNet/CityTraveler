using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class CoordinatesModel : Entity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public virtual AddressModel Address { get; set; }
    }
}
