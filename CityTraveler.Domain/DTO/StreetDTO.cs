using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class StreetShowDTO
    {
        public Guid Id { set; get; }
        public string Title { get; set; }
    }
    public class StreetGetDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<CoordinatesDTO> Coordinates { get; set; }
    }

    public class StreetDTO
    {
        public Guid Id { set; get; }
        public string Description { get; set; }
        public string Title { get; set; }
        public IEnumerable<CoordinatesDTO> Coordinates { get; set; }
    }
}
