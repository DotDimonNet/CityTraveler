using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    [Serializable]
    public class TripControllerException:Exception
    {
        public TripControllerException() : base() { }
        public TripControllerException(string message) : base(message) { }
        public TripControllerException(string message, Exception innerException) : base(message, innerException) { }
        public TripControllerException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
