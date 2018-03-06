using LightContainer.Interfaces;

namespace LightContainer.Bases
{
    /// <summary>
    /// Registers dependencies with application context.
    /// </summary>
    public abstract class InjectionModule : IInjectionModule
    {
        #region Methods

        /// <summary>
        /// Loads dependencies into the dependency registry that is from the application context.
        /// </summary>
        /// <param name="registrar">Dependency registry for regsitering dependencies.</param>
        public abstract void Load(IRegistrar registrar);

        #endregion
    }
}
