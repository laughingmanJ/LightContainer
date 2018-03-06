

using System;
using System.Collections.Generic;

namespace LightContainer.Interfaces
{
    /// <summary>
    /// Map of defined factories for an IoC container.
    /// </summary>
    public interface IFactoryMap
    {
        #region Methods

        void Add(Type interfaceKey, string identity, IInjectionFactory factory);

        bool ContainsKey(Type interfaceKey, string name = "");

        IInjectionFactory GetFactory(Type interfaceType, string name = "");

        IEnumerable<IInjectionFactory> GetFactories(Type interfaceType);

        #endregion
    }
}
