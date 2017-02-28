using LightContainer.Interfaces;
using System;

namespace LightContainer.Configuration
{
    /// <summary>
    /// Configuration of a regsitered class.
    /// </summary>
    class ClassConfiguration : IClassConfiguration, IClassConfigurationInternal
    {
        #region IClassConfigurationInternal Properties

        /// <summary>
        /// Gets the registered name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Class type.
        /// </summary>
        public IConstructorInternal Constructor { get; private set; }

        /// <summary>
        /// Class properties configuration.
        /// </summary>
        public Type Type { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes with registered class type. 
        /// </summary>
        /// <param name="type">Registered class type.</param>
        /// <param name="type">Registered name for class.</param>
        public ClassConfiguration(Type type, string name)
        {
            Type = type ?? throw new ArgumentNullException("type");
            Name = name ?? throw new ArgumentNullException("name");

            Constructor = new Constructor(type);
        }

        #endregion

        #region IClassConfiguration Methods

        /// <summary>
        /// Accesses the constructor configuration. 
        /// </summary>
        /// <returns>Reference to the constructor configuration.</returns>
        public IConstructor WithConstructor()
        {
            return Constructor;
        }

        #endregion
    }
}
