using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class Image : IImage
    {
        public string Source { get ; set ; }
        public IEnumerable<string> Tags { get ; set ; }
        public Guid Id { get ; set; }
        public DateTime Created { get ; set ; }
        public DateTime Modified { get ; set ; }
        public string Title { get ; set ; }
        public string Description { get ; set ; }

        public Image(string source, IEnumerable<string> tag, Guid guid, DateTime created, DateTime modified, string title, string descriptiom)
        {
            Source = source;
            Tags = tag;
            Id = guid;
            Created = created;
            Modified = modified;
            Title = title;
            Description = descriptiom;
        }
    }
}
