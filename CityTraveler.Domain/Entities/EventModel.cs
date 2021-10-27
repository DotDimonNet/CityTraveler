using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class EventModel : EntertaimentModel
    {
        //To JSON
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
