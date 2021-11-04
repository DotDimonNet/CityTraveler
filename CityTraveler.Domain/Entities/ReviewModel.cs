using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class ReviewModel : Entity, IDescribable
    {
        public virtual Guid? RatingId { get; set; }
        public virtual RatingModel Rating { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual ApplicationUserModel User { get; set; }
        public virtual ICollection<ReviewImageModel> Images { get; set; } = new List<ReviewImageModel>();
        public virtual ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class EntertainmentReviewModel : ReviewModel 
    {
        public virtual Guid EntertainmentId { get; set; }
        public virtual EntertaimentModel Entertaiment { get; set; }
    }
    public class TripReviewModel : ReviewModel 
    {
        public virtual TripModel Trip { get; set; }
        public virtual Guid TripId { get; set; }
    }
    /*public class RatingReviewModel : ReviewModel
    {
        public virtual Guid RatingId { get; set; }
        public virtual RatingModel Rating { get; set; }
    }*/
}
