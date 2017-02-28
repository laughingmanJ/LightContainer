using LightContainer.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LightContainer.Core
{
    public class ApplicationContext : IApplicationContext
    {
        #region Fields

        private readonly static TypeInfo _moduleInterfaceInfo = typeof(IInjectionModule).GetTypeInfo();

        private readonly IIocContainerInternal _container;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the application context class.
        /// </summary>
        public ApplicationContext()
        {
            _container = new IocContainer();
        }

        #endregion

        #region IApplicationContext Methods

        public IIocContainer GetContainer()
        {
            return _container;
        }

        public void Load(string searchPattern)
        {
            var files = Directory.GetFiles(AppContext.BaseDirectory, searchPattern,
                SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var asm = Assembly.Load(new AssemblyName(fileName));

                var moduleTypes = asm.GetTypes()
                    .Where(type => _moduleInterfaceInfo.IsAssignableFrom(type));

                foreach (var moduleType in moduleTypes)
                {
                    var module = (IInjectionModule)Activator.CreateInstance(moduleType);
                    Load(module);
                }
            }
        }

        public void Load(params IInjectionModule[] modules)
        {
            foreach (var module in modules)
            {
                module.Load(_container);
            }
        }

        #endregion
    }
}
