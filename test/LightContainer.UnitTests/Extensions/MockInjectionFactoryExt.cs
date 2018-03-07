using LightContainer.Interfaces;
using Moq;

namespace LightContainer.UnitTests.Extensions
{
    public static class MockInjectionFactoryExt
    {
        public static void SetupCreate(this Mock<IInjectionFactory> mockFactory, object instance)
        {
            mockFactory.Setup(mock => mock.Create(It.IsAny<IIocContainer>()))
                .Returns(instance);
        }
    }
}
