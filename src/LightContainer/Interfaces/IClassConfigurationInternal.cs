using System;


namespace LightContainer.Interfaces
{
    /// <summary>
    /// Internal configuration of a regsitered class.
    /// </summary>
    interface IClassConfigurationInternal : IClassConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets the registered name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets class type.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Class constructor configuration.
        /// </summary>
        IConstructorInternal Constructor { get; }

        #endregion
    }
}
