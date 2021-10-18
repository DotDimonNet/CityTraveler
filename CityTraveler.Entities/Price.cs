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
        public double Tax { get; set; }
        public double VAT { get; set; }
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public string Title { get ; set; }
        public string Description { get; set; }
        public double Value { get; set; }

        public Price(double tax, double vat, string title, string description, double price)
        {
            Tax = tax;
            VAT = vat;
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            Modified = DateTime.Now;
            Title = title;
            Description = description;
            Value = price;
        }

        public Price()
        {

        }
    }
}
