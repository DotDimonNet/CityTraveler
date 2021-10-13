using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class Price : IPrice
    {
        public double Tax { get ; set ; }
        public double VAT { get ; set ; }
        public Guid Id { get ; set ; }
        public DateTime Created { get ; set ; }
        public DateTime Modified { get ; set ; }
        public string Title { get ; set; }
        public string Description { get ; set ; }
        double IPrice.Price { get ; set ; }

        public Price(double tax, double vat, Guid guid, DateTime created, DateTime modified, string title, string description, IPrice price)
        {
            Tax = tax;
            VAT = vat;
            Id = guid;
            Created = created;
            Modified = modified;
            Title = title;
            Description = description;
            Price = price;

        }
    }
}
