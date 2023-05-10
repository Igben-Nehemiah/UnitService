using System;
using System.Runtime.Serialization;
using UnitService.Core.Models;

namespace UnitService.Core.Exceptions
{

    [Serializable]
    internal class DimensionNotFoundException : ApplicationException
    {
        public DimensionNotFoundException()
            : base("Dimension not found in registry") { }

        public DimensionNotFoundException(Dimension dimension)
            : base($"Dimension '{dimension}' not found in registry") { }

        public DimensionNotFoundException(string message, Exception innerException) 
            : base(message, innerException) { }

        protected DimensionNotFoundException(SerializationInfo info, StreamingContext context) 
            : base(info, context) { }
    }
}
