using System;
using System.Runtime.Serialization;
using UnitService.Core.Models;

namespace UnitService.Core.Exceptions
{
    [Serializable]
    internal sealed class DimensionParseException : ApplicationException
    {
        public DimensionParseException()
           : base("Could not parse string to dimension") { }

        public DimensionParseException(string dimension)
            : base($"Could not parse string '{dimension}'to dimension") { }

        public DimensionParseException(string message, Exception innerException)
            : base(message, innerException) { }

        protected DimensionParseException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}