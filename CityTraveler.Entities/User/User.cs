﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Entities
{
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public IEnumerable<string> SavedWays { get; set; } // change IEnumerable 
        public IEnumerable<string> FavouritePlace { get; set; } // change IEnumerable 
        /// IEnumerable<Way> UncompetedWays { get; set; } 
        public UserRole  Role {get; set;}


        





    }
}
