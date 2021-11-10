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
        public virtual Guid? ReviewId { get; set; }
        public virtual ApplicationUserModel User { get; set; }
        public virtual Guid UserId { set; get; }
        public double Value { get; set; }
    }
}
