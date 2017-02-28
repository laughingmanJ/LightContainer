using System;
using System.Collections.Generic;
using System.Text;

namespace LightContainer.Interfaces
{
    public interface IInjectable
    {
        void Inject(IIocContainer container, string name);
    }
}
