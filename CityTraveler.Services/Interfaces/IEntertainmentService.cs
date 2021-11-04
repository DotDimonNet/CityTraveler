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
        public IEnumerable<EntertainmentShowDTO> GetAllDTO(int typeId = 0);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByTitle(string title, int typeId = 0);
        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByTitle(string title, int typeId = 0);
        public IEnumerable<EntertaimentModel> GetEntertainments(IEnumerable<Guid> guids, int typeId = 0);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTO(IEnumerable<Guid> guids, int typeId = 0);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByStreet(string streetTitle, int typeId = 0);
        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByStreet(string streetTitle, int typeId = 0);
        public IEnumerable<EntertaimentModel> GetEntertainmentsByCoordinates(CoordinatesDTO coordinates, int typeId = 0);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByCoordinates(CoordinatesDTO coordinates, int typeId = 0);
        public Task<EntertaimentModel> GetEntertainmentById(Guid guids, int typeId = 0);
        public Task<EntertainmentShowDTO> GetEntertainmentDTOById(Guid guids, int typeId = 0);
        public Task<EntertaimentModel> GetEntertainmentByAddress(AddressGetDTO address, int typeId = 0);
        public Task<EntertainmentShowDTO> GetEntertainmentDTOByAddress(AddressGetDTO address, int typeId = 0);
        public double GetAverageRating(EntertaimentModel entertaiment);
    }
}
