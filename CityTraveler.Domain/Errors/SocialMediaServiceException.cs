using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    public class SocialMediaServiceException: Exception
    {
        public SocialMediaServiceException() : base() { }
        public SocialMediaServiceException(string message) : base(message) { }
        public SocialMediaServiceException(string message, Exception innerException) : base(message, innerException) { }
        public SocialMediaServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
