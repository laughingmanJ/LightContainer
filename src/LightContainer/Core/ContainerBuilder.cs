using LightContainer.Configuration;
using LightContainer.Factories;
using LightContainer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LightContainer.Core
{
    public class ContainerBuilder : IContainerBuilder
    {
        #region Constants

        private const string TypeExistsErrorMessage = "Type is already registered in Inversion of Control container.";

        #endregion

        #region Fields

        // Map of type factories.
        private readonly IFactoryMap _factoryMap;

        private readonly static TypeInfo _moduleInterfaceInfo = typeof(IInjectionModule).GetTypeInfo();

        #endregion

        #region Constructors

        public ContainerBuilder()
        {
            _factoryMap = new FactoryMap();
        }

        #endregion

        #region IContainerBuilder Methods

        public void RegisterInstance<T>(T instance)
            where T : class
        {
            RegisterInstance(instance, "");
        }

        public void RegisterInstance<T>(T instance, string name)
            where T : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }

            if (IsRegistered<T>(name))
            {
                throw new TypeLoadException(TypeExistsErrorMessage);
            }

            var factory = new SingletonFactory(instance);
            _factoryMap.Add(typeof(T), name, factory);
        }

        public IConstructor RegisterInstance<T, TR>()
            where T : class
            where TR : T
        {
            return RegisterInstance<T, TR>("");
        }

        public IConstructor RegisterInstance<T, TR>(string name)
            where T : class
            where TR : T
        {
            if (IsRegistered<T>(name))
            {
                throw new TypeLoadException(TypeExistsErrorMessage);
            }

            var type = typeof(TR);

            var configuration = new ClassConfiguration(type, name);
            var factory = new LazySingletonFactory(type, configuration);

            _factoryMap.Add(typeof(T), name, factory);
            return configuration.Constructor;
        }

        public IConstructor RegisterType<T, TR>()
            where T : class
            where TR : T
        {
            return RegisterType<T, TR>("");
        }

        public IConstructor RegisterType<T, TR>(string name)
            where T : class
            where TR : T
        {
            if (IsRegistered<T>(name))
            {
                throw new TypeLoadException(TypeExistsErrorMessage);
            }

            var type = typeof(TR);

            var configuration = new ClassConfiguration(type, name);
            var factory = new ClassFactory(type, configuration);

            _factoryMap.Add(typeof(T), name, factory);
            return configuration.Constructor;
        }

        /// <summary>
        /// Indicates if a type is registered with the registry.
        /// </summary>
        /// <returns>Value indicating the type is registered with the registry.</returns>
        public bool IsRegistered<T>(string name = "")
            where T : class
        {
            var interfaceType = typeof(T);
            return _factoryMap.ContainsKey(interfaceType, name);
        }

        public IIocContainer Build()
        {
            var container = new IocContainer(_factoryMap);

            //foreach (var module in _modules)
            //{
            //    module.Load(container);
            //}
            return container;
        }

        public void RegisterModules(string searchPattern)
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
                    //Load(module);
                }
            }
        }

        public void RegisterModules(params IInjectionModule[] modules)
        {
            foreach (var module in modules)
            {
                //_modules.Add(module);
            }
        }

        #endregion
    }
}
