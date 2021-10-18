using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class Entertaiment : Entity, IDescribable
    {
        public virtual ICollection<EntertaimentImageModel> Images { get; set; } = new List<EntertaimentImageModel>();
        public virtual ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
        public virtual EntertaimentAddressModel Address { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
