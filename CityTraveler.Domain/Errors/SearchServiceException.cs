using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    [Serializable]
    public class SearchServiceException : Exception
    {
        public SearchServiceException() : base() { }
        public SearchServiceException(string message) : base(message) { }
        public SearchServiceException(string message, Exception innerException) : base(message, innerException) { }
        public SearchServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }


    }
}
