using LightContainer.Interfaces;
using System;

namespace LightContainer.Configuration
{
    /// <summary>
    /// Concrete value parameter class.
    /// </summary>
    class ValueParameter : IParameter
    {
        #region Public Properties

        /// <summary>
        /// Parameter Type.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Parameter value.
        /// </summary>
        public object Value { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes class with type and value.
        /// </summary>
        /// <param name="type">Parameter type.</param>
        /// <param name="value">Parameter value.</param>
        public ValueParameter(Type type, object value)
        {
            Type = type ?? throw new ArgumentNullException("type");
            Value = value;
        }

        #endregion
    }
}
