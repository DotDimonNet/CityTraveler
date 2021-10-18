using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class StreetModel : Entity, IDescribable
    {
        public virtual ICollection<StreetAddressModel> Addresses { get; set; } = new List<StreetAddressModel>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
