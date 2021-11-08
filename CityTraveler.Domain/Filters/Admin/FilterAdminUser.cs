using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Filters
{
    public class FilterAdminUser
    {
        public string UserName { get; set; } = "";
        public string Gender { get; set; } = "";
        public  string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public  bool LockoutUser { get; set; } = false;

    }
}
