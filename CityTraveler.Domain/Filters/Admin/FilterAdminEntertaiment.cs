using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Filters.Admin
{
    public class FilterAdminEntertaiment
    {
        public string Street { get; set; }
        public int Type { get; set; } = -1;
        public double AveragePriceMore { get; set; }
        public double AveragePriceLess { get; set; }
        public double AverageRatingMore { get; set; }
        public double AverageRatingLess { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
