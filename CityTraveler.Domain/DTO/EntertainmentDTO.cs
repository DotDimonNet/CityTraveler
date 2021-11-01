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
        public AddressModel Address { get; set; }
        public int Type { get; set; }
        public EntertaimentPriceModel AveragePrice { get; set; }
        public ICollection<EntertaimentImageModel> Images { get; set; } = new List<EntertaimentImageModel>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
