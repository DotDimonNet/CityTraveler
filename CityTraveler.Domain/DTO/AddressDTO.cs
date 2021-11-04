using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class AddressShowDTO
    {
        public string HouseNumber { set; get; }
        public string ApartmentNumber { set; get; }
        public StreetShowDTO Street { set; get; }
    }
    public class AddressGetDTO
    {
        public string HouseNumber { set; get; }
        public string ApartmentNumber { set; get; }
        public CoordinatesDTO Coordinates { get; set; }
        public string StreetId { get; set; }
    }
}
