using System;
using LightContainer.Interfaces;

namespace LightContainer.Factories
{
    /// <summary>
    /// Factory that holds single instances.
    /// </summary>
    class SingletonFactory : IInjectionFactory, IDisposable
    {
        #region Fields

        private readonly object _instance;

        #endregion

        #region Constructors

        public SingletonFactory(object instance)
        {
            _instance = instance ?? throw new ArgumentNullException("instance");
        }

        #endregion

        #region IInjectionFactory Methods

        /// <summary>
        /// Creates an instance of an object.
        /// </summary>
        /// <returns>Instance of a object.</returns>
        public object Create(IIocContainer container)
        {
            return _instance;
        }

        #endregion

        #region IDisposable Methods

        public void Dispose()
        {
            if (_instance != null)
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
