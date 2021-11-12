using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface ITimeLineService
    {
        public Task<List<EntertaimentModel>> CreateTimeLine(IEnumerable<EntertainmentGetDTO> entertainments);
        public void MarkAsCompleted(Guid tripId, Guid entrtainmentId);
        public void MArkAsInProgress(Guid tripId, Guid entrtainmentId);


    }
}
