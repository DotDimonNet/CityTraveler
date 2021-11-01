using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class ReviewDTO
    {
        public RatingModel Rating { get; set; }
        //what should we do with this field? It should be proccesed with userManagesr
        public ApplicationUserModel User { get; set; }
        public ICollection<ReviewImageModel> Images { get; set; } = new List<ReviewImageModel>();
        public ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
