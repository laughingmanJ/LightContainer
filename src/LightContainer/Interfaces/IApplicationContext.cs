

namespace LightContainer.Interfaces
{
    public interface IApplicationContext
    {
        IIocContainer BuildContainer();

        void Load(params IInjectionModule[] modules);

        void Load(string searchPattern);
    }
}
