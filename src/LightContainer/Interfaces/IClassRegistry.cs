

namespace LightContainer.Interfaces
{
    /// <summary>
    /// Registry of defined classes.
    /// </summary>
    public interface IClassRegistry
    {
        #region Methods

        void RegisterInstance<T>(T instance)
            where T : class;

        void RegisterInstance<T>(T instance, string name)
            where T : class;

        IConstructor RegisterInstance<T, TR>()
            where T : class
            where TR : T;

        IConstructor RegisterInstance<T, TR>(string name)
            where T : class
            where TR : T;

        IConstructor RegisterType<T, TR>()
            where T : class
            where TR : T;

        IConstructor RegisterType<T, TR>(string name)
            where T : class
            where TR : T;

        #endregion
    }
}
