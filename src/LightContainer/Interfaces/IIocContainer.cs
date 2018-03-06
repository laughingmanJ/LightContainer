using System;
using System.Collections.Generic;

namespace LightContainer.Interfaces
{
    /// <summary>
    /// Represents an inversion of control container.  
    /// </summary>
    public interface IIocContainer
    {
        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);

        object Resolve(Type type, string name = "");

        T Resolve<T>()
            where T : class;

        T Resolve<T>(string name = "") 
            where T : class;

        IEnumerable<object> ResolveAll(Type type);

        IEnumerable<T> ResolveAll<T>()
            where T : class;

        #endregion
    }
}
