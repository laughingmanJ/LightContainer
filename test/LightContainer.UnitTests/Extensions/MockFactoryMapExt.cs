using LightContainer.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightContainer.UnitTests.Extensions
{
    public static class MockFactoryMapExt
    {
        public static void SetupFactory<T>(this Mock<IFactoryMap> mockFactoryMap, Mock<IInjectionFactory> mockFactory,
            string identity = "")
        {
            mockFactoryMap.Setup(mock => mock.ContainsKey(typeof(T), identity))
                .Returns(true);
            mockFactoryMap.Setup(mock => mock.GetFactory(typeof(T), identity))
                .Returns(mockFactory.Object);
        }
    }
}
