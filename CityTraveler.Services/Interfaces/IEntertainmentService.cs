using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Services.Interfaces
{
    public interface IEntertainmentService : IServiceMetadata
    {
        public IEnumerable<EntertainmentShowDTO> GetAllDTO();
        public IEnumerable<EntertaimentModel> GetEntertainmentsByTitle(string title);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByTitle(string title);
        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> guids);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTO(IEnumerable<Guid> guids);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByStreet(string streetTitle);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByCoordinates(CoordinatesDTO coordinates);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByCoordinates(CoordinatesDTO coordinates);
        public Task<EntertaimentModel> GetEntertainmentById(Guid guids);
        public Task<EntertainmentShowDTO> GetEntertainmentDTOById(Guid guids);
        public Task<EntertaimentModel> GetEntertainmentByAddress(AddressGetDTO address);
        public Task<EntertainmentShowDTO> GetEntertainmentDTOByAddress(AddressGetDTO address);
        public double GetAverageRating(EntertaimentModel entertaiment);
    }
}
