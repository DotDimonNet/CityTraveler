using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class ReviewImageDTO: ImageGetDTO
    {
        public Guid ReviewId { get; set; }
    }
}
