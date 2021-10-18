using CityTraveler.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class ContactInfo : IContactInfo
    {
        public string Phone { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Links { get; set; }
        public ContactInfo(string phone, string websiteURl, string email, IEnumerable<string> links) 
        {
            Phone = phone;
            WebsiteUrl = websiteURl;
            Email = email;
            Links = links;
        }

        public ContactInfo()
        {

        }
    }
}
