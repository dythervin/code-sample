using Dythervin.AutoAttach;
using Dythervin.SerializedReference.Refs;
using UnityEngine;
using Zenject;

namespace Game.View.Utils.Installers
{
    [RequireComponent(typeof(Context))]
    public class ChildInstallers : MonoInstaller
    {
        [Attach(Attach.ZenjectContext, FilterMethodName = nameof(Filter)), SerializeField]
        private RefList<IInstaller> installers;
        
        [Attach(Attach.ZenjectContext), SerializeField]
        private RefList<IZenInstaller> customInstallers;

        private bool Filter(object obj)
        {
            return !ReferenceEquals(this, obj);
        }

        public override void InstallBindings()
        {
            foreach (IInstaller installer in installers)
            {
                Container.Inject(installer);
                installer.InstallBindings();
            }
            
            foreach (IZenInstaller installer in customInstallers)
            {
                installer.InstallBindings(Container);
            }
        }

        private void Reset()
        {
            TryGetComponent(out Context context);
            context.Installers = new[] { this };
        }
    }
}