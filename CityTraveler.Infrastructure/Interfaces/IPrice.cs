using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace CityTraveler.Infrastructure.Interfaces
{
    public interface IPrice : IDbData, IDescribable
    {
        public double Price { get; set; }
        public double Tax { get; set; }
        public double VAT { get; set; }
        public double Total => Price * (1 + VAT) + Tax;
        public bool IsValid => Price >= 0 && VAT >= 0 && VAT <= 3;
    }
}
