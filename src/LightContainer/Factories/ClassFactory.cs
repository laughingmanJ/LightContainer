using LightContainer.Configuration;
using LightContainer.Helpers;
using LightContainer.Interfaces;
using System;


namespace LightContainer.Factories
{
    /// <summary>
    /// Factory for creating dependency types. 
    /// </summary>
    class ClassFactory : IInjectionFactory
    {
        #region Fields

        // Configuration for constructing the type.
        private readonly IClassConfigurationInternal _configuration;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes class with the type and a type dependency resolver.
        /// </summary>
        /// <param name="type">Type.</param>
        public ClassFactory(Type type, IClassConfigurationInternal configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region IFactory Methods

        /// <summary>
        /// Creates an instance of an object.
        /// </summary>
        /// <returns>Instance of a object.</returns>
        public virtual object Create(IIocContainer container)
        {
            var instance = CallConstructor(container);
            return instance;
        }

        private object CallConstructor(IIocContainer container)
        {
            // When there are no constrictor parameters, just use the Activator to call the default constructor.  
            if (_configuration.Constructor.ParameterCount == 0 )
            {
                return Activator.CreateInstance(_configuration.Type);
            }

            var parameterValues = new object[_configuration.Constructor.ParameterCount];
            var index = 0;

            foreach (var parameter in _configuration.Constructor.Parameters)
            {
                if (parameter is ValueParameter)
                {
                    parameterValues[index++] = ((ValueParameter)parameter).Value;
                }
                else if (parameter is ReferenceParameter)
                {
                    var refParameter = (ReferenceParameter)parameter;
                    parameterValues[index++] = container.Resolve(refParameter.Type, refParameter.Identity);
                }
                else if (parameter is ReferenceAllParameter)
                {
                    var refAllParameter = (ReferenceAllParameter)parameter;
                    var instances = container.ResolveAll(refAllParameter.InnerType);
                    parameterValues[index++] = EnumerableCast.Cast(refAllParameter.InnerType, instances);
                }
            }

            return _configuration.Constructor.Invoke(parameterValues);
        }

        #endregion
    }
}
