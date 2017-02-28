using LightContainer.Configuration;
using LightContainer.Factories;
using LightContainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LightContainer.Core
{
    /// <summary>
    /// Inversion of control container.
    /// </summary>
    public class IocContainer : IIocContainerInternal
    {
        #region Constants

        private const string TypeExistsErrorMessage = "Type is already registered in Inversion of Control container.";

        #endregion

        #region Fields

        // Map of type factories.
        private readonly IDictionary<Type, IDictionary<string, IInjectionFactory>> _typeFactories;

        #endregion

        #region Constructors

        public IocContainer()
        {
            _typeFactories = new Dictionary<Type, IDictionary<string, IInjectionFactory>>();
        }

        #endregion

        #region IIocContainer Methods

        public object Resolve(Type type)
        {
            return Resolve(type,"");
        }

        public object Resolve(Type interfaceType, string name = "")
        {
            try
            {
                if (_typeFactories.ContainsKey(interfaceType) && _typeFactories[interfaceType].ContainsKey(name))
                {
                    var factory = _typeFactories[interfaceType][name];
                    return factory.Create(this);
                }

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IoC container is unable to resolve type: {0} – error: {1}", interfaceType.Name,
                                ex.Message);
                return null;
            }
        }

        public T Resolve<T>() where T : class
        {
            return Resolve<T>("");
        }

        public T Resolve<T>(string name = "") where T : class
        {
            var type = typeof(T);
            return (T)Resolve(type, name);
        }

        public IEnumerable<object> ResolveAll(Type interfaceType)
        {
            try
            {
                var instances = new List<object>();

                if (_typeFactories.ContainsKey(interfaceType))
                {
                    foreach (var factorySet in _typeFactories[interfaceType])
                    {
                        var instance = factorySet.Value.Create(this);
                        instances.Add(instance);
                    }
                }

                return instances;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("IoC container is unable to resolve type: {0} – error: {1}", interfaceType.Name,
                                ex.Message);
                return null;
            }
        }

        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            var type = typeof(T);
            return ResolveAll(type).Cast<T>();
        }

        #endregion

        #region IClassRegistry Methods

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
            AddFactory(typeof(T), name, factory);
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

            AddFactory(typeof(T), name, factory);
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

            AddFactory(typeof(T), name, factory);
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
            if (!_typeFactories.ContainsKey(interfaceType))
            {
                return false;
            }

            return _typeFactories[interfaceType].ContainsKey(name);
        }

        #endregion

        #region  IDisposable Methods

        public void Dispose()
        {
            foreach (var keyValuePair in _typeFactories)
            {
                var disposable = keyValuePair.Value as IDisposable;

                if(disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        #endregion

        #region Private Methods

        private void AddFactory(Type interfaceKey, string identity, IInjectionFactory factory)
        {
            if (!_typeFactories.ContainsKey(interfaceKey))
            {
                _typeFactories.Add(interfaceKey, new Dictionary<string, IInjectionFactory>());
            }

            _typeFactories[interfaceKey].Add(identity, factory);
        }

        #endregion
    }
}
