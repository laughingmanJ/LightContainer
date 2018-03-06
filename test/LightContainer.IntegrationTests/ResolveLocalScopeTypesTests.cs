using LightContainer.Core;
using LightContainer.IntegrationTests.TestInterfaces;
using LightContainer.IntegrationTests.TestTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LightContainer.IntegrationTests
{
    public class ResolveLocalScopeTypesTests
    {
        [Fact]
        public void ResolveDefaultConstructorType()
        {
            // Arrange

            // Register the test type with its default constructor.
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<ITest1, Test1>();

            var container = containerBuilder.Build();

            // Act

            // Resolve two instances from the container for the ITest1 interface.
            var instance1 = container.Resolve<ITest1>();
            var instance2 = container.Resolve<ITest1>();

            // Assert

            // Ensure that both are different objects and not a reference to the same one.
            Assert.NotEqual(instance1.Id, instance2.Id);
            Assert.Equal(45, instance1.TestCall());
            Assert.Equal(45, instance2.TestCall());
        }
    }
}
