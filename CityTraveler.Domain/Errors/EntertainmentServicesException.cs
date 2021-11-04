using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    [Serializable]
    public class EntertainmentServicesException : Exception
    {
        public EntertainmentServicesException() : base() { }
        public EntertainmentServicesException(string message) : base(message) { }
        public EntertainmentServicesException(string message, Exception innerException) : base(message, innerException) { }
        public EntertainmentServicesException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
