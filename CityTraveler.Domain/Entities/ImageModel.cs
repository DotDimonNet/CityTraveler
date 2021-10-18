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
        //public IEnumerable<string> Tags { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UserImageModel : ImageModel
    {
        public virtual ApplicationUserModel User { get; set; }
    }

    public class EntertaimentImageModel : ImageModel
    {
        public virtual Entertaiment Entertaiment { get; set; }
    }

    public class ReviewImageModel : ImageModel
    {
        public virtual ReviewModel Review { get; set; }
    }

    public class TripImageModel : ImageModel
    {
        public virtual TripModel Trip { get; set; }
    }
}
