


namespace LightContainer.Interfaces
{
    /// <summary>
    /// Interface for type constructor.
    /// </summary>
    public interface IConstructor
    {
        #region Methods

        /// <summary>
        /// Adds a parameter with a concrete value to the constructors’ parameter list.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="value">Concrete value</param>
        /// <returns>A reference of the constructor for chaining calls. </returns>
        IConstructor WithValueParameter<T>(T value);

        /// <summary>
        /// Adds a parameter with a reference identity that will help load a dependent type to the constructors’ parameter list. 
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="identity">Reference identity of dependent type.</param>
        /// <returns>A reference of the constructor for chaining calls. </returns>
        IConstructor WithRefParameter<T>(string identity = "")
            where T : class;


        /// <summary>
        /// Adds a parameter with a reference all for a specific interface that will help load a collection of dependent types of 
        /// that interface to the constructors’ parameter list. 
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <returns>A reference of the constructor for chaining calls. </returns>
        IConstructor WithRefAllParameter<T>()
            where T : class;

        #endregion
    }
}
