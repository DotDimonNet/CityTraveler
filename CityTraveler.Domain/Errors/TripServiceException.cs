using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    [Serializable]
    public class TripServiceException:Exception
    {
        public TripServiceException() : base(){ }
        public TripServiceException(string message): base(message) { }
        public TripServiceException(string message, Exception innerException):base(message, innerException) { }
        public TripServiceException (SerializationInfo info, StreamingContext context):base(info, context) { }
       

    }
}
