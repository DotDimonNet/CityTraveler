using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Enums
{
    public class PlaceType : Enumeration
    {
        public static readonly PlaceType Event = new(1, "Event");
        public static readonly PlaceType Institution = new(2, "Institution");
        public static readonly PlaceType Landscape = new(3, "Landscape");

        protected PlaceType(int id, string name) : base(id, name) { }
    }
}
