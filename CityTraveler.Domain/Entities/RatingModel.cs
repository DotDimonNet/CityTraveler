using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class RatingModel : Entity
    {
        public virtual ReviewModel Review { get; set; }
        public double Value { get; set; }
    }
}
