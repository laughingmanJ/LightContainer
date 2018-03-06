using System;
using System.Collections.Generic;
using System.Text;

namespace LightContainer.Interfaces
{
    public interface IContainerBuilder : IRegistrar
    {
        #region Methods

        IIocContainer Build();

        void RegisterModules(params IInjectionModule[] modules);

        void RegisterModules(string searchPattern);

        #endregion
    }
}
