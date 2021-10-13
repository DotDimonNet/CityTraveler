using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IDescribable
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
