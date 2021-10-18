using System;
using System.ComponentModel.DataAnnotations;

namespace CityTraveler.Domain.Entities
{
    public abstract class Entity : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
