using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;

namespace CityTraveler.Services
{
    public class MapService : IMapService
    {
        ApplicationContext _context;
        public MapService(ApplicationContext context)
        {
            _context = context;
        }

        public bool IsActive { get; set; }
        public string Version { get; set; }

        void IMapService.BuildConnection()
        {
            int maxSteps = 5;
            int minConnectionCount = 1;

        }
    }
}
