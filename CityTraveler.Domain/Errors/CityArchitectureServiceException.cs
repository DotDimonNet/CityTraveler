using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    [Serializable]
    public class  CityArchitectureServiceException: Exception
    {
        public CityArchitectureServiceException() : base() { }
        public CityArchitectureServiceException(string message) : base(message) { }
        public CityArchitectureServiceException(string message, Exception innerException) : base(message, innerException) { }
        public CityArchitectureServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
