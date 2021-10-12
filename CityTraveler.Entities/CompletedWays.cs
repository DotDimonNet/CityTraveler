using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    class CompletedWays
    {
     
        string Name { get; set; }

        string Description { get; set; }

        // Need to change type of Feedback
        string Feedback { get; set; }

        int Cost { get; set; }

        DateTime PostingDate { get; set; }

    }
}
