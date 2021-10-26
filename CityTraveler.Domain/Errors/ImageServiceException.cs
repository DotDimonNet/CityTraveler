using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    [Serializable]
    public class ImageServiceException: Exception
    {
        public ImageServiceException() : base(){ }
        public ImageServiceException (string message) : base(message) { }
        public ImageServiceException(string message, Exception innerException):base(message, innerException) { }
        public ImageServiceException(SerializationInfo info, StreamingContext context):base(info, context) { }
    }
}
