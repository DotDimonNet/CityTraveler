using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Filters.Admin
{
    public class FilterAdminReview
    {
        public virtual double RatingLess { get; set; }
        public virtual double RatingMore { get; set; }
        public virtual string User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
