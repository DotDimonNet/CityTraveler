using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities.DTO
{
    public class UpdateRoleModel
    {
        public string UserName { get; set; }
        public UserRole Role { get; set; }
    }
}
