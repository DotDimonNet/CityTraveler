using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Enums
{
    public enum EntertainmentType
    {
        Landscape = 1,
        Institution = 2,
        Event = 3,
    }

    /*public class EntertainmentType : Enumeration
    {
        public static readonly EntertainmentType Landskape = new(1, "Landskape");
        public static readonly EntertainmentType Institution = new(2, "Institution");
        public static readonly EntertainmentType Event = new(3, "Event");

        protected EntertainmentType(int id, string name) : base(id, name) { }
    }*/
}
