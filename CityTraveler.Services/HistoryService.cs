using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Services.Interfaces;
using CityTraveler.Infrastucture.Data;

namespace CityTraveler.Services 
{
    class HistoryService : IHistoryService
    {
        //last trip, 
        private ApplicationContext _context;
        public HistoryService(ApplicationContext context)
        {
            _context = context;
        }

    }
}
