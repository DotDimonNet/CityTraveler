using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class FilterUsers
    {
        public virtual UserProfileModel Profile { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
        public virtual ICollection<TripModel> Trips { get; set; } = new List<TripModel>();

    }
}
