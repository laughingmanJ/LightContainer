using LightContainer.Interfaces;
using System;

namespace LightContainer.Configuration
{
    /// <summary>
    ///  Reference parameter class for all dependency types under one interface.
    /// </summary>
    class ReferenceAllParameter : IParameter
    {
        #region Public Properties

        /// <summary>
        /// Parameter Type (Collection).
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Type within the collection.
        /// </summary>
        public Type InnerType { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes class with type and reference identity.
        /// </summary>
        /// <param name="type">Parameter type (Collection) for constructor.</param>
        /// <param name="innerType">The type inside the collection.</param>
        public ReferenceAllParameter(Type type, Type innerType)
        {
            Type = type ?? throw new ArgumentNullException("type");
            InnerType = innerType ?? throw new ArgumentNullException("innerType");
        }

        #endregion
    }
}
