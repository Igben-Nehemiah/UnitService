using System;
using System.Runtime.Serialization;
using UnitService.Core.Models;

namespace UnitService.Core.Exceptions
{
    [Serializable]
    internal sealed class InvalidReferenceUnitRelationshipException : ApplicationException
    {
        public InvalidReferenceUnitRelationshipException()
            : base("Dimension not found in registry") { }

        public InvalidReferenceUnitRelationshipException(Unit unit)
            : base($"Unit '{unit}' has an invalid reference unit relationship" +
                  $": Scale -> {unit.ReferenceUnitRelationship.Scale}; " +
                  $"Offset -> {unit.ReferenceUnitRelationship.Offset}") { }

        public InvalidReferenceUnitRelationshipException(string message, Exception innerException)
            : base(message, innerException) { }

        protected InvalidReferenceUnitRelationshipException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
