using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class ImageGetDTO : ImageDTO
    {
        public bool IsMain { get; set; }
        public string Description { get; set; }
    }
  
    public class ImageDTO
    {
        public string Source { get; set; }
        public string Title { get; set; }
    }
}
