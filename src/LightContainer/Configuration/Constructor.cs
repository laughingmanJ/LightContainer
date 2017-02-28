using LightContainer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LightContainer.Configuration
{
    /// <summary>
    /// Constructor for type.
    /// </summary>
    class Constructor : IConstructorInternal
    {
        #region Fields

        // Informaton of the type that uses this constructor.
        private readonly TypeInfo _typeInfo;

        // List of parameters for the constructor
        private readonly IList<IParameter> _parameters;

        // Cache of type signature created when the constructor is first invoked for performance.
        private Type[] _parameterTypesCache;

        #endregion

        #region Properties

        /// <summary>
        /// Constructor’s parameters.
        /// </summary>
        public IEnumerable<IParameter> Parameters
        {
            get {  return _parameters;}
        }

        /// <summary>
        /// The number of constructor parameters.
        /// </summary>
        public int ParameterCount { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes class.
        /// </summary>
        public Constructor()
        {
            _parameters = new List<IParameter>();          
        }

        /// <summary>
        /// Initializes class with type.
        /// </summary>
        /// <param name="type">Type.</param>
        public Constructor(Type type)
        {
            _typeInfo = type.GetTypeInfo() ?? throw new ArgumentNullException("type");
            _parameters = new List<IParameter>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a parameter with a concrete value to the constructors’ parameter list.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="value">Concrete value</param>
        /// <returns>A reference of the constructor for chaining calls. </returns>
        public IConstructor WithValueParameter<T>(T value)
        {
            var parameter = new ValueParameter(typeof(T), value);
            _parameters.Add(parameter);
            ParameterCount++;
            return this;
        }

        /// <summary>
        /// Adds a parameter with a reference identity that will help load a dependent type to the constructors’ parameter list. 
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="identity">Reference identity of dependent type.</param>
        /// <returns>A reference of the constructor for chaining calls. </returns>
        public IConstructor WithRefParameter<T>(string identity = "")
            where T : class 
        {
            if (identity == null)
            {
                identity = "";
            }

            var parameter = new ReferenceParameter(typeof(T), identity);
            _parameters.Add(parameter);
            ParameterCount++;
            return this;
        }

        /// <summary>
        /// Adds a parameter with a reference all for a specific interface that will help load a collection of dependent types of 
        /// that interface to the constructors’ parameter list. 
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <returns>A reference of the constructor for chaining calls. </returns>
        public IConstructor WithRefAllParameter<T>()
            where T : class
        {
            var parameter = new ReferenceAllParameter(typeof(IEnumerable<T>) ,typeof(T));
            _parameters.Add(parameter);
            ParameterCount++;
            return this;            
        }

        /// <summary>
        /// Invokes the constructor with parameter values to return an instance of the type. 
        /// </summary>
        /// <param name="parameterValues">Parameter values</param>
        /// <returns>Instance of the type</returns>
        public object Invoke(object[] parameterValues)
        {
            if (_parameterTypesCache == null)
            {
                _parameterTypesCache = _parameters
                    .Select(item => item.Type)
                    .ToArray();
            }

            var ctr = _typeInfo.GetConstructor(_parameterTypesCache);

            if (ctr == null)
            {
                throw new NotSupportedException("Constructor with assigned parameters was not found on this type.");
            }

            return ctr.Invoke(parameterValues);
        }

        #endregion
    }
}
