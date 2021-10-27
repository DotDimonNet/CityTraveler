using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class FilterEntertainment
    {
        public virtual AddressModel Address { get; set; }
        public virtual EntertainmentType Type { get; set; }
        public virtual ICollection<TripModel> Trips { get; set; } = new List<TripModel>();
        public virtual ICollection<EntertaimentPriceModel> Prices { get; set; } = new List<EntertaimentPriceModel>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
