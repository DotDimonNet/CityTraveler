using System;

namespace CityTraveler.Domain.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
    }
}
