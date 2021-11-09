using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Entities
{
    public class AddressModel : Entity
    {
        public virtual Guid CoordinatesId { get; set; }
        public virtual CoordinatesAddressModel Coordinates { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public virtual Guid StreetId { get; set; }
        public virtual StreetModel Street { get; set; }
        public virtual EntertaimentModel Entertaiment { get; set; }
    }

}
