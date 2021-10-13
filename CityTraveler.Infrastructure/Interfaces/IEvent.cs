using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IEvent : IEntertaiment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
