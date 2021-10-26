using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Domain.Entities
{
    public class ImageModel : Entity, IDescribable
    {
        public string Source { get; set; }
        public bool IsMain { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UserImageModel : ImageModel
    {
        public virtual Guid UserId { get; set; } 
        public virtual ApplicationUserModel User { get; set; }
    }

    public class EntertaimentImageModel : ImageModel
    {
        public virtual Guid EntertaimentId { get; set; }
        public virtual EntertaimentModel Entertaiment { get; set; }
    }

    public class ReviewImageModel : ImageModel
    {
        public virtual Guid ReviewId { get; set; }
        public virtual ReviewModel Review { get; set; }
    }

    public class TripImageModel : ImageModel
    {
        public virtual Guid TripId { get; set; } 
        public virtual TripModel Trip { get; set; }
    }
}
