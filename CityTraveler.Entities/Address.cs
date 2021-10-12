using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTravel.Entities
{ 
    public class Address
    {
        public string Country { set; get; }
        public string City { set; get; }
        public string Street { set; get; }
        public string HouseNumber { set; get; }
    }
}