using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities.DTO
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public UserRole Role { get; set; }
        public string AvatarSrc { get; set; }
        public string PasswordHash { get; set; }
    }
}
