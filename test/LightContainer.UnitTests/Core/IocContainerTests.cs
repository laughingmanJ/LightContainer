using LightContainer.Core;
using LightContainer.Interfaces;
using LightContainer.UnitTests.Extensions;
using LightContainer.UnitTests.TestInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LightContainer.UnitTests.Core
{
    public class IocContainerTests
    {
        [Fact]
        public void ResolveType_Unregistered()
        {
            // Arrange ////

            // Create a mock factory map that the container will use.
            var mockFactoryMap = new Mock<IFactoryMap>();

            // Create an instance of the container with the mock factory map.
            var container = new IocContainer(mockFactoryMap.Object);

            // Act ////
            var instance = container.Resolve<ITest1>();

            // Assert ////
            Assert.Null(instance);
        }

        [Fact]
        public void ResolveType_With_Default_Identity()
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
            mockFactory.SetupCreate(mockTestType.Object);

            // Create a mock factory map that the container will use.
            var mockFactoryMap = new Mock<IFactoryMap>();
            mockFactoryMap.SetupFactory<ITest1>(mockFactory);

            // Create an instance of the container with the mock factory map.
            var container = new IocContainer(mockFactoryMap.Object);

            // Act ////
            var instance = container.Resolve<ITest1>();


            // Assert ////
            Assert.NotNull(instance);
            Assert.Equal(testId, instance.Id);
        }

        [Fact]
        public void ResolveType_With_Specified_Identity()
        {
            // Arrange ////

            const string typeIdentifier = "TypeTestId1";

            // Test variables for Moq objects to make sure the container finds and resolves the right type.
            var testId = Guid.NewGuid();

            // Create a test mock for the resolved instance.
            var mockTestType = new Mock<ITest1>();
            mockTestType.Setup(mock => mock.Id)
                .Returns(testId);

            // Create a mock factory to hold the test mock and return it when the container calls create.
            var mockFactory = new Mock<IInjectionFactory>();
            mockFactory.SetupCreate(mockTestType.Object);

            // Create a mock factory map that the container will use.
            var mockFactoryMap = new Mock<IFactoryMap>();
            mockFactoryMap.SetupFactory<ITest1>(mockFactory, typeIdentifier);

            // Create an instance of the container with the mock factory map.
            var container = new IocContainer(mockFactoryMap.Object);

            // Act ////
            var instance = container.Resolve<ITest1>(typeIdentifier);

            // Assert ////
            Assert.NotNull(instance);
            Assert.Equal(testId, instance.Id);
        }

        [Fact]
        public void ResolveMultiples()
        {
            // Arrange ////

            // Test variables for Moq objects to make sure the container finds and resolves the right type.
            var testId1 = Guid.NewGuid();
            var testId2 = Guid.NewGuid();
            var testId3 = Guid.NewGuid();

            var factories = new List<IInjectionFactory>();

            var mockTestType1 = new Mock<ITest1>();
            mockTestType1.Setup(mock => mock.Id)
                .Returns(testId1);

            var mockFactory1= new Mock<IInjectionFactory>();
            mockFactory1.SetupCreate(mockTestType1.Object);
            factories.Add(mockFactory1.Object);

            var mockTestType2 = new Mock<ITest1>();
            mockTestType2.Setup(mock => mock.Id)
                .Returns(testId2);

            var mockFactory2 = new Mock<IInjectionFactory>();
            mockFactory2.SetupCreate(mockTestType2.Object);
            factories.Add(mockFactory2.Object);


            var mockTestType3 = new Mock<ITest1>();
            mockTestType3.Setup(mock => mock.Id)
                .Returns(testId3);

            var mockFactory3 = new Mock<IInjectionFactory>();
            mockFactory3.SetupCreate(mockTestType3.Object);
            factories.Add(mockFactory3.Object);

            var mockFactoryMap = new Mock<IFactoryMap>();
            mockFactoryMap.Setup(mock => mock.GetFactories(typeof(ITest1)))
                .Returns(factories);

            // Create an instance of the container with the mock factory map.
            var container = new IocContainer(mockFactoryMap.Object);

            // Act ////
            var instances = container.ResolveAll<ITest1>();

            // Assert ////
            Assert.Equal(3, instances.Count());
            Assert.Contains(instances, ins => ins.Id == testId1);
            Assert.Contains(instances, ins => ins.Id == testId2);
            Assert.Contains(instances, ins => ins.Id == testId3);
        }

    }
}
