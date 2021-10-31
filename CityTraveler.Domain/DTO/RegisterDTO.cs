using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public string AvatarSrc { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
