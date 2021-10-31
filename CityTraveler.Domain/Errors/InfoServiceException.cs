using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    public class InfoServiceException: Exception
    {
        public InfoServiceException() : base() { }
        public InfoServiceException(string message) : base(message) { }
        public InfoServiceException(string message, Exception innerException) : base(message, innerException) { }
        public InfoServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
