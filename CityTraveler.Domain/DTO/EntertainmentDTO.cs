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
        public EntertainmentAddressDTO Address { get; set; }
        public string StreetId { get; set; }
        public int Type { get; set; }
        public string PriceTitle { get; set; }
        public double PriceValue { get; set; }
        public IEnumerable<string> ImageSources { get; set; } = new List<string>();
        public IEnumerable<bool> IsMain { get; set; } = new List<bool>();
        public IEnumerable<string> ImageTitles { get; set; } = new List<string>();
        public IEnumerable<string> ImageDescription { get; set; } = new List<string>();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
    }
}
