using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace CityTraveler.Domain.Entities
{
    public class ApplicationUserModel : IdentityUser<Guid>
    {
        public virtual Guid UserId { get; set; }
        public virtual UserProfileModel Profile { get; set; }
        public virtual ICollection<UserImageModel> Images { get; set; } = new List<UserImageModel>();
        public virtual ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
        public virtual ICollection<TripModel> Trips { get; set; } = new List<TripModel>();
        
    }

    public class ApplicationUserRole : IdentityRole<Guid>
    {
        public ApplicationUserRole() : base()
        {
        }

        public ApplicationUserRole(string roleName) : base(roleName)
        {
        }
    }
}
