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
    class IocContainer : IIocContainer
    {

        #region Fields

        // Map of type factories.
        private readonly IFactoryMap _factoryMap;

        #endregion

        #region Constructors

        internal IocContainer(IFactoryMap factoryMap)
        {
            _factoryMap = factoryMap;
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
                if (_factoryMap.ContainsKey(interfaceType, name))
                {
                    var factory = _factoryMap.GetFactory(interfaceType, name);
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

                var factories = _factoryMap.GetFactories(interfaceType);

                foreach (var factory in factories)
                {
                    var instance = factory.Create(this);
                    instances.Add(instance);
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
    }
}
