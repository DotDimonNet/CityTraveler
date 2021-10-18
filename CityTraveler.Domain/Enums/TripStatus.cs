using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Enums
{
    public class TripStatus : Enumeration
    {
        public static readonly TripStatus New = new(1, "New");
        public static readonly TripStatus InProgress = new(2, "InProgress");
        public static readonly TripStatus Passed = new(3, "Passed");

        protected TripStatus(int id, string name) : base(id, name) { }
    }
}
