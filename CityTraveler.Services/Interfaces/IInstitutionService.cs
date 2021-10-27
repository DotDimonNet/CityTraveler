using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;

namespace CityTraveler.Services.Interfaces
{
    public interface IInstitutionService : IServiceMetadata
    {
        public IEnumerable<EntertaimentModel> GetAll();
        public IEnumerable<EntertaimentModel> GetInstitutions(IEnumerable<Guid> guids);
        public Task<EntertaimentModel> GetInstitutionById(Guid guids);
        public Task<EntertaimentModel> GetInstitutionByTitle(string title);
        public IEnumerable<EntertaimentModel> GetInstitutionsByStreet(StreetModel street);
        public IEnumerable<EntertaimentModel> GetInstitutionsByStreet(string streetTitle);
        public Task<EntertaimentModel> GetInstitutionByCoordinates(CoordinatesModel coordinates);
        public Task<EntertaimentModel> GetInstitutionByAddress(AddressModel address);
        public Task<EntertaimentModel> GetInstitutionByAddress(string houseNumber, string apartmentNumber, string streetTitle);
    }
}
