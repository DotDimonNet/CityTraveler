using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using CityTraveler.Infrastructure.Enums;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IInstitution : IEntertaiment
    {
        public InstitutionType Type { get; set; }
    }
}
