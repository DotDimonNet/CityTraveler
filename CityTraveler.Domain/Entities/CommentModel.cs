using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class CommentModel : Entity
    {
        public virtual ApplicationUserModel Owner { get; set; }
        public virtual Guid ReviewId { get; set; }
        public virtual ReviewModel Review { get; set; }
        public virtual CommentStatus Status { get; set; }
        public string Description { get; set; }
    }
}
