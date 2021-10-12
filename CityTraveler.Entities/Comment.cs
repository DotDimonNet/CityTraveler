using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityTravel.Entities
{
	public class Comment
    {
        //text, OwnerId, date, ID
        public string Text { set; get; }
        public Guid OwnerId { set; get; }
        public DateTime Date { set; get; }
        public Guid ID { set; get; }
    }
}