using LightContainer.IntegrationTests.TestInterfaces;
using System;

namespace LightContainer.IntegrationTests.TestTypes
{
    public class Test1 : ITest1
    {
        public Guid Id { get; private set; }

        public Test1()
        {
            Id = Guid.NewGuid();
        }

        public int TestCall()
        {
            return 45;
        }
    }
}
