using LightContainer.Interfaces;
using System;


namespace LightContainer.Factories
{
    /// <summary>
    /// Factory for single instances that are lazy loaded.
    /// </summary>
    class LazySingletonFactory : ClassFactory, IDisposable
    {
        #region Fields

        // Lock for lazy loading instance.
        private readonly  object _lock = new object();

        // Actual instance when loaded.
        private object _instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes class with type of instance.  
        /// </summary>
        /// <param name="type">Instance type.</param>
        public LazySingletonFactory(Type type, IClassConfigurationInternal configuration)
            :base(type,configuration)
        {
        }

        #endregion

        #region IInjectionFactory Methods

        /// <summary>
        /// Creates an instance of an object.
        /// </summary>
        /// <returns>Instance of a object.</returns>
        public override object Create(IIocContainer container)
        {
            lock (_lock)
            {
                // If this is the first time being accessed, initialize the instance.  
                if (_instance == null)
                {
                    _instance = base.Create(container);
                }

                return _instance;
            }
        }

        #endregion

        #region IDisposable Methods

        public void Dispose()
        {
            if(_instance != null)
            {
                if (_instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }

        #endregion
    }
}
