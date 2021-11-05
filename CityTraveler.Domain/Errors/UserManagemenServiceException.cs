using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    public class UserManagemenServiceException: Exception
    {
        public UserManagemenServiceException() : base() { }
        public UserManagemenServiceException(string message) : base(message) { }
        public UserManagemenServiceException(string message, Exception innerException) : base(message, innerException) { }
        public UserManagemenServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
