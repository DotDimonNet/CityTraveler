using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class FilterUsers
    {
        public string UserName { get; set; } = "";
        public string Gender { get; set; } = "";
        public string EntertainmentName { get; set; } = "";
    }
}
