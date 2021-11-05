using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Filters.Admin
{
    public class FilterAdminEntertaiment
    {
        public string Street { get; set; } = "";
        public int Type { get; set; } = -1;
        public double AveragePriceMore { get; set; } = 0;
        public double AveragePriceLess { get; set; } = 10000;
        public double AverageRatingMore { get; set; } = 0;
        public double AverageRatingLess { get; set; } = 5;
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
    }
}
