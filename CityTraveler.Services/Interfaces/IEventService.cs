using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;

namespace CityTraveler.Services.Interfaces
{
    public interface IEventService : IEntertainmentService
    {
        public IEnumerable<EntertaimentModel> GetEventByBeginingDay(DateTime begin);
    }
}
