using System;
using System.Runtime.Serialization;
using UnitService.Core.Models;

namespace UnitService.Core.Exceptions
{
    [Serializable]
    internal sealed class DimensionsMismatchException : ApplicationException
    {
        public DimensionsMismatchException()
            : base("The Dimensions operated on do not match") { }

        public DimensionsMismatchException(string message)
            : base(message) { }

        public DimensionsMismatchException(string message, Exception innerException)
            : base(message, innerException) { }

        protected DimensionsMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
