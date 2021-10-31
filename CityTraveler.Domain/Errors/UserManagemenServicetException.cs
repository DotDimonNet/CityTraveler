using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler.Domain.Errors
{
    public class UserManagemenServicetException: Exception
    {
        public UserManagemenServicetException() : base() { }
        public UserManagemenServicetException(string message) : base(message) { }
        public UserManagemenServicetException(string message, Exception innerException) : base(message, innerException) { }
        public UserManagemenServicetException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
