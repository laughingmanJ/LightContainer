using System;
using System.Collections.Generic;
using System.Text;

namespace LightContainer.Interfaces
{
    interface IConstructorInternal : IConstructor
    {
        object Invoke(object[] parameterValues);

        IEnumerable<IParameter> Parameters { get; }

        int ParameterCount { get; }
    }
}
