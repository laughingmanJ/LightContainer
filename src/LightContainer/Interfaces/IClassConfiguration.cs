

namespace LightContainer.Interfaces
{
    /// <summary>
    /// Configuration of a regsitered class.
    /// </summary>
    public interface IClassConfiguration
    {
        #region Methods

        /// <summary>
        /// Accesses the constructor configuration. 
        /// </summary>
        /// <returns>Reference to the constructor configuration.</returns>
        IConstructor WithConstructor();

        #endregion
    }
}
