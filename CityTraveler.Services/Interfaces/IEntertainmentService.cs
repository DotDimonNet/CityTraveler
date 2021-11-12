using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Enums;

namespace CityTraveler.Services.Interfaces
{
    public interface IEntertainmentService
    {
        public IEnumerable<EntertainmentShowDTO> GetAllDTO(EntertainmentType typeId = EntertainmentType.All);
        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByTitle(string title, EntertainmentType typeId = EntertainmentType.All);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTO(IEnumerable<Guid> guids, EntertainmentType typeId = EntertainmentType.All);
        public IEnumerable<EntertainmentPreviewDTO> GetEntertainmentsDTOByStreet(string streetTitle, EntertainmentType typeId = EntertainmentType.All);
        public IEnumerable<EntertainmentShowDTO> GetEntertainmentsDTOByCoordinates(CoordinatesDTO coordinates, EntertainmentType typeId = EntertainmentType.All);
        public Task<EntertainmentShowDTO> GetEntertainmentDTOByIdAsync(Guid guids, EntertainmentType typeId = EntertainmentType.All);
        public Task<EntertainmentShowDTO> GetEntertainmentDTOByAddressAsync(AddressGetDTO address, EntertainmentType typeId = EntertainmentType.All);
        public double GetAverageRating(EntertaimentModel entertaiment);
    }
}
