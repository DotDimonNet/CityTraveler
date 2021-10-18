using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class UserProfileModel : Entity
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        //public IEnumerable<string> Links { get; set; } = new List<string>();
        public virtual ApplicationUserModel User { get; set; }
    }
}
