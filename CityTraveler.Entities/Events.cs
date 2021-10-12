using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    class Events
    {
        public Events()
        {

        }
        string Name { get; set; }

        // Need to change adress
        string Adress { get; set; }



        // Need to change type of coordinates
        int Coordinates { get; set; }

        string Description { get; set; }

        DateTime StartWork { get; set; }
        DateTime EndWork { get; set; }

        // Need to change type of feedback
        string Feedback { get; set; }

        int Price { get; set; }


    }
}
