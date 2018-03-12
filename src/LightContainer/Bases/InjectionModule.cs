using LightContainer.Interfaces;

namespace LightContainer.Bases
{
    /// <summary>
    /// Registers module dependencies with container builder.
    /// </summary>
    public abstract class InjectionModule : IInjectionModule
    {
        #region Methods

        /// <summary>
        /// Loads dependencies into the passed in dependency registry.
        /// </summary>
        /// <param name="registrar">Dependency registry for regsitering dependencies.</param>
        public abstract void Load(IRegistrar registrar);

        #endregion
    }
}
