using LightContainer.Configuration;
using LightContainer.UnitTests.TestInterfaces;
using Xunit;

namespace LightContainer.UnitTests.Configuration
{
    public class ClassConfigurationTests
    {
        private const string TestConstructorName = "Test CTR";

        [Fact]
        public void Setup_Constructor()
        {
            // Arrange ////
            var classConfiguration = new ClassConfiguration(typeof(ITest1), TestConstructorName);

            // Act ////
            var ctr = classConfiguration.WithConstructor();

            // Assert ////
            Assert.NotNull(ctr);
        }

    }
}
