using CityTraveler.Domain.Entities;
using CityTraveler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class CommentDTO
    {
        //public ApplicationUserModel Owner { get; set; }
        public Guid ReviewId { get; set; }
        //public ReviewDTO Review { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
    }
}
