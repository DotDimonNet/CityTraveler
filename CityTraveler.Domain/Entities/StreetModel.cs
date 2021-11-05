using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class StreetModel : Entity, IDescribable
    {
        public virtual ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();
        public string Title { get; set; }
        public string Description { get; set; }
        public double StreetBeginningY { get; set; }
        public double StreetBeginningX { get; set; }
        public double StreetEndingY { get; set; }
        public double StreetEndingX { get; set; }
    }
}
