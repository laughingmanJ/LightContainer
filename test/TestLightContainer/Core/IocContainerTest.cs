using LightContainer.Core;
using Xunit;

namespace TestLightContainer.Core
{
    public class IocContainerTest
    {
        [Fact]
        public void Register_and_Resolve_Instance()
        {
            var container = new IocContainer();

            //container.RegisterInstance<ITestInstance>()
        }
    }
}
