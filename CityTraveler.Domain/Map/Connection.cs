using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityTraveler.Domain.Entities;

namespace CityTraveler.Domain.Map
{
    public class Connection
    {
        public Connection(CoordinatesModel point1, CoordinatesModel point2)
        { 
            Point1 = point1;
            Point2 = point2;
            //calculate distance in meters
            Lenght = Math.Sqrt(Math.Pow((point1.Latitude - point2.Latitude) * 111.134, 2) 
                + Math.Pow((point1.Longitude - point2.Longitude) * 111.321, 2)); 
        }
        public CoordinatesModel Point1 { get; set; }
        public CoordinatesModel Point2 { get; set; }
        public double Lenght { get; set; }
    }
}
