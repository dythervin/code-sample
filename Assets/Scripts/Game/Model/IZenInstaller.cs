using Zenject;

namespace Game
{
    public interface IZenInstaller
    {
        void InstallBindings(DiContainer container);
    }
}