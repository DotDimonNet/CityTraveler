using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Enums
{
    public class CommentStatus : Enumeration
    {
        public static readonly CommentStatus Liked = new(1, "Liked");
        public static readonly CommentStatus Suprised = new(2, "Suprised");
        public static readonly CommentStatus OMG = new(3, "OMG");

        protected CommentStatus(int id, string name) : base(id, name) { }
    }
}
