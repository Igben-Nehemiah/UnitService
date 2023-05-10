using System;
using System.Runtime.Serialization;
using UnitService.Core.Models;

namespace UnitService.Core.Exceptions
{
    [Serializable]
    internal class UnitNotFoundException : ApplicationException
    {
        public UnitNotFoundException()
           : base("Unit not found in registry") { }

        public UnitNotFoundException(Unit unit)
            : base($"Unit '{unit}' not found in registry") { }

        public UnitNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        protected UnitNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
