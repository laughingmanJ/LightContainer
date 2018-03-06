using LightContainer.Core;
using LightContainer.Interfaces;
using LightContainer.UnitTests.TestInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LightContainer.UnitTests.Core
{
    public class IocContainerTests
    {
        [Fact]
        public void Resolve_Default_Instance_With_Default_Constructor()
        {
            // Arrange ////

            // Test variables for Moq objects to make sure the container finds and resolves the right type.
            var testId = Guid.NewGuid();

            // Create a test mock for the resolved instance.
            var mockTestType = new Mock<ITest1>();
            mockTestType.Setup(mock => mock.Id)
                .Returns(testId);

            // Create a mock factory to hold the test mock and return it when the container calls create.
            var mockFactory = new Mock<IInjectionFactory>();
            mockFactory.Setup(mock => mock.Create(It.IsAny<IIocContainer>()))
                .Returns(mockTestType.Object);

            // Create a mock factory map that the 
            var mockFactoryMap = new Mock<IFactoryMap>();
            mockFactoryMap.Setup(mock => mock.ContainsKey(It.IsAny<Type>(), It.IsAny<string>()))
                .Returns(true);
            mockFactoryMap.Setup(mock => mock.GetFactory(typeof(ITest1), ""))
                .Returns(mockFactory.Object);

            // Create an instance of the container with the mock factory map.
            var container = new IocContainer(mockFactoryMap.Object);

            // Act ////
            var instance = container.Resolve<ITest1>();


            // Assert ////
            Assert.NotNull(instance);
            Assert.Equal(testId, instance.Id);
        }
    }
}
