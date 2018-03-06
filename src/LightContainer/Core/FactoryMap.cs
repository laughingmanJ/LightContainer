using LightContainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightContainer.Core
{
    class FactoryMap : IFactoryMap
    {
        // Map of type factories.
        private readonly IDictionary<Type, IDictionary<string, IInjectionFactory>> _typeFactories = 
            new Dictionary<Type, IDictionary<string, IInjectionFactory>>();

        public void Add(Type interfaceKey, string identity, IInjectionFactory factory)
        {
            if (!_typeFactories.ContainsKey(interfaceKey))
            {
                _typeFactories.Add(interfaceKey, new Dictionary<string, IInjectionFactory>());
            }

            _typeFactories[interfaceKey].Add(identity, factory);
        }

        public bool ContainsKey(Type interfaceKey, string name = "")
        {
            if (!_typeFactories.ContainsKey(interfaceKey))
            {
                return false;
            }

            return _typeFactories[interfaceKey].ContainsKey(name);
        }

        public IInjectionFactory GetFactory(Type interfaceType, string name = "")
        {
            if (_typeFactories.ContainsKey(interfaceType) && _typeFactories[interfaceType].ContainsKey(name))
            {
                var factory = _typeFactories[interfaceType][name];
                return factory;
            }

            // TODO: Thow a custom exception that the type does not have a registered factory.
            throw new Exception("");
        }

        public IEnumerable<IInjectionFactory> GetFactories(Type interfaceType)
        {
            if (_typeFactories.ContainsKey(interfaceType))
            {
                return _typeFactories[interfaceType]
                    .Select(pair => pair.Value)
                    .ToList();
            }

            return new List<IInjectionFactory>();
        }
    }
}
