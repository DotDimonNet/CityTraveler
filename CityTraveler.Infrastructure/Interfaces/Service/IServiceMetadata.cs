﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IServiceMetadata : IDbData, IDescribable
    {
        public bool IsActive { get; set; }
        public string Version { get; set; }
    }   
}

