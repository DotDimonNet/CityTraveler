using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;
using CityTraveler.Domain.DTO;

namespace CityTraveler.Services.Interfaces
{
    public interface IInstitutionService : IServiceMetadata
    {
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetInstitutions(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetInstitutionById(Guid guids);
        public IEnumerable<EntertaimentModel> GetInstitutionsByTitle(string title);
        public IEnumerable<EntertaimentModel> GetInstitutionsByStreet(string streetTitle);
        public Task<EntertaimentModel> GetInstitutionByCoordinates(CoordinatesDTO coordinates);
        public Task<EntertaimentModel> GetInstitutionByAddress(AddressDTO address);
    }
}
