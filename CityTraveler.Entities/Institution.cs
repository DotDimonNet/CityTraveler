using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    class Institution
    {
        public Institution()
        {

        }
        string Name { get; set; }


        // Need to change tepe of adress
        string Adress { get; set; }
        string Descroption { get; set; }

        // Need to change type of Contact Info
        string ConactInfo { get; set; }

        // Need to change type of  rating
        string Rating { get; set; }




        // Need to change type of feedback
        string Feedback { get; set; }

        //Need to change type of  coordinates
        int Coordinates { get; set; }

        int Cost { get; set; }
        string Site { get; set; }

        string Type { get; set; }
    }
}
