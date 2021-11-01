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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string HouseNumber { set; get; }
        public string ApartmentNumber { set; get; }
        public string StreetTitle { get; set; }
        public int Type { get; set; }
        public string PriceTitle { get; set; }
        public double PriceValue { get; set; }
        public ICollection<string> Sourses { get; set; } = new List<string>();
        public ICollection<bool> IsMain { get; set; } = new List<bool>();
        public ICollection<string> ImageTitles { get; set; } = new List<string>();
        public ICollection<string> ImageDescription { get; set; } = new List<string>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
