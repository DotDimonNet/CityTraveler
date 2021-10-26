using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;

namespace CityTraveler.Domain.DTO
{
    public class EntertainmentDTO
    {
        public virtual AddressModel Address { get; set; }
        public virtual EntertainmentType Type { get; set; }
        public virtual ICollection<EntertaimentPriceModel> Prices { get; set; } = new List<EntertaimentPriceModel>();
        public virtual ICollection<EntertaimentImageModel> Images { get; set; } = new List<EntertaimentImageModel>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
