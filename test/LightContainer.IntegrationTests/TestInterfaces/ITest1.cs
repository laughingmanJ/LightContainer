using System;
using System.Collections.Generic;
using System.Text;

namespace LightContainer.IntegrationTests.TestInterfaces
{
    public interface ITest1
    {
        Guid Id { get; }

        int TestCall();
    }
}
