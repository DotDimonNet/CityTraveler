using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public interface IDescribable
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
