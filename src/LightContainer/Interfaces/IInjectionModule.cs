using System;
using System.Collections.Generic;
using System.Text;

namespace LightContainer.Interfaces
{
    public interface IInjectionModule
    {
        void Load(IRegistrar registrar);
    }
}
