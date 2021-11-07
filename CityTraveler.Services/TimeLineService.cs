using AutoMapper;
using CityTraveler.Domain.DTO;
using CityTraveler.Domain.Enums;
using CityTraveler.Domain.Entities;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services
{
    public class TimeLineService : ITimeLineService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<TimeLineService> _logger;
        private readonly IMapper _mapper;

        public TimeLineService(ApplicationContext context, IMapper mapper, ILogger<TimeLineService> logger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public TimeLineService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool IsActive { get; set; }
        public string Version { get; set; }

        public async Task<List<EntertaimentModel>> CreateTimeLine(IEnumerable<EntertainmentGetDTO> entertainments)
        {
            try
            {
                var timeLine = new List<EntertaimentModel>();

                foreach (var entertainment in entertainments)
                {
                    var streetId = Guid.Parse(entertainment.Address.StreetId);
                    var elemet = _mapper.Map<EntertainmentGetDTO, EntertaimentModel>(entertainment);
                    elemet.Address.Street = await _context.Streets.FirstOrDefaultAsync(x => x.Id == streetId);
                    elemet.Type = (EntertainmentType)entertainment.Type;
                    timeLine.Add(elemet);
                }

                return timeLine; 
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: {e.Message}");
                throw new Exception($"Exception on creating Timeline! {e.Message}");
            }
        }

        public void MarkAsCompleted(Guid tripId, Guid entrtainmentId)
        {
            throw new NotImplementedException();
        }

        public void MArkAsInProgress(Guid tripId, Guid entrtainmentId)
        {
            throw new NotImplementedException();
        }
    }
}
