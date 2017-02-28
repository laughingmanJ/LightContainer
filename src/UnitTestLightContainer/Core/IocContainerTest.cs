using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightContainer.Core;
using UnitTestLightContainer.TestInterfaces;

namespace UnitTestLightContainer
{
    [TestClass]
    public class IocContainerTest
    {
        [TestMethod]
        public void Register_and_Resolve_Instance()
        {
            var container = new IocContainer();

            //container.RegisterInstance<ITestInstance>()
        }
    }
}
