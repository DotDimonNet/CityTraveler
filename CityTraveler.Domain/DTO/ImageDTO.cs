using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class ImageGetDTO
    {
        public string Source { get; set; }
        public bool IsMain { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
  
    public class ImageShowDTO
    {
        public string Source { get; set; }
        public string Title { get; set; }
    }
}
