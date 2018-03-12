using LightContainer.Interfaces;
using Moq;

namespace LightContainer.UnitTests.Extensions
{
    /// <summary>
    /// Injection Factory interface extension methods.
    /// </summary>
    public static class MockInjectionFactoryExt
    {
        /// <summary>
        /// Use to quickly mock the create method for a IInjectionFactory mock.
        /// </summary>
        /// <param name="mockFactory">IInjectionFactory mock.</param>
        /// <param name="instance">Instance of the object that should be returned when the create method is called.</param>
        public static void SetupCreate(this Mock<IInjectionFactory> mockFactory, object instance)
        {
            mockFactory.Setup(mock => mock.Create(It.IsAny<IIocContainer>()))
                .Returns(instance);
        }
    }
}
