using CityTraveler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class TimeLineService : ITimeLineService
    {
        public bool IsActive { get; set; }
        public string Version { get; set; }
    }
}
