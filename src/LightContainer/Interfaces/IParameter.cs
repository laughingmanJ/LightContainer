using System;

namespace LightContainer.Interfaces
{
    /// <summary>
    /// Constructor parameter.
    /// </summary>
    interface IParameter
    {
        #region Properties

        /// <summary>
        /// Parameter Type.
        /// </summary>
        Type Type { get; }

        #endregion
    }
}
