using System;
using System.Collections.Generic;
using System.Text;

namespace LightContainer.Interfaces
{
    public interface IApplicationContext
    {
        IIocContainer GetContainer();

        void Load(params IInjectionModule[] modules);

        void Load(string searchPattern);
    }
}
