using System;
using LightContainer.Interfaces;

namespace LightContainer.Configuration
{
    /// <summary>
    /// Reference parameter class for dependency types.
    /// </summary>
    class ReferenceParameter : IParameter
    {
        #region Public Properties

        /// <summary>
        /// Parameter Type.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Parameter reference identity.
        /// </summary>
        public string Identity { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes class with type and reference identity.
        /// </summary>
        /// <param name="type">Parameter type.</param>
        /// <param name="identity">Reference identity</param>
        public ReferenceParameter(Type type, string identity)
        {
            Type = type ?? throw new ArgumentNullException("type");
            Identity = identity ?? throw new ArgumentNullException("identity");
        }

        #endregion
    }
}
