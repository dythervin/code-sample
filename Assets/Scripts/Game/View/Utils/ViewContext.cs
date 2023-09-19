using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;
using Zenject.Internal;

namespace Game.View.Utils
{
    internal class ViewContext : RunnableContext
    {
        public event Action PreInstall;
        public event Action PostInstall;
        public event Action PreResolve;
        public event Action PostResolve;
        
        [SerializeField]
        [Tooltip("Note that this field is optional and can be ignored in most cases.  This is really only needed if you want to control the 'Script Execution Order' of your subcontainer.  In this case, define a new class that derives from MonoKernel, add it to this game object, then drag it into this field.  Then you can set a value for 'Script Execution Order' for this new class and this will control when all ITickable/IInitializable classes bound within this subcontainer get called.")]
        private MonoKernel kernel;

        private DiContainer _container;

        public override DiContainer Container => _container;

        public override IEnumerable<GameObject> GetRootGameObjects()
        {
            return new[] { gameObject };
        }

        [Inject]
        public void Construct(
            DiContainer newContainer)
        {
            Assert.IsNull(_container);
            Assert.IsNull(newContainer.DefaultParent);
            _container = newContainer;
            Initialize();
        }

        protected override void RunInternal()
        {
            Install();
            ResolveAndStart();
        }

        public void Install() 
        {
            // Do this after creating DiContainer in case it's needed by the pre install logic
            PreInstall?.Invoke();

            var injectableMonoBehaviours = new List<MonoBehaviour>();

            GetInjectableMonoBehaviours(injectableMonoBehaviours);

            foreach (var instance in injectableMonoBehaviours)
            {
                if (instance is MonoKernel)
                {
                    Assert.That(ReferenceEquals(instance, kernel),
                        "Found MonoKernel derived class that is not hooked up to GameObjectContext.  If you use MonoKernel, you must indicate this to GameObjectContext by dragging and dropping it to the Kernel field in the inspector");
                }

                _container.QueueForInject(instance);
            }

            _container.IsInstalling = true;

            try
            {
                InstallBindings(injectableMonoBehaviours);
            }
            finally
            {
                _container.IsInstalling = false;
            }

            PostInstall?.Invoke();
        }

        private void ResolveAndStart() 
        {
            PreResolve?.Invoke();

            _container.ResolveRoots();

            PostResolve?.Invoke();

            // Normally, the IInitializable.Initialize method would be called during MonoKernel.Start
            // However, this behaviour is undesirable for dynamically created objects, since Unity
            // has the strange behaviour of waiting until the end of the frame to call Start() on
            // dynamically created objects, which means that any GameObjectContext that is created
            // dynamically via a factory cannot be used immediately after calling Create(), since
            // it will not have been initialized
            // So we have chosen to diverge from Unity behaviour here and trigger IInitializable.Initialize
            // immediately - but only when the GameObjectContext is created dynamically.  For any
            // GameObjectContext's that are placed in the scene, we still want to execute
            // IInitializable.Initialize during Start()
            if (gameObject.scene.isLoaded && !_container.IsValidating)
            {
                kernel = _container.Resolve<MonoKernel>();
                kernel.Initialize();
            }
        }

        protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
        {
            ZenUtilInternal.AddStateMachineBehaviourAutoInjectersUnderGameObject(gameObject);

            // We inject on all components on the root except ourself
            foreach (var monoBehaviour in GetComponents<MonoBehaviour>())
            {
                if (monoBehaviour == null)
                {
                    // Missing script
                    continue;
                }

                if (!ZenUtilInternal.IsInjectableMonoBehaviourType(monoBehaviour.GetType()))
                {
                    continue;
                }

                if (monoBehaviour == this)
                {
                    continue;
                }

                monoBehaviours.Add(monoBehaviour);
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                if (child != null)
                {
                    ZenUtilInternal.GetInjectableMonoBehavioursUnderGameObject(
                        child.gameObject, monoBehaviours);
                }
            }
        }

        private void InstallBindings(List<MonoBehaviour> injectableMonoBehaviours)
        {
            _container.DefaultParent = transform;

            _container.Bind<Context>().FromInstance(this);
            _container.Bind<ViewContext>().FromInstance(this);

            if (kernel == null)
            {
                _container.Bind<MonoKernel>()
                    .To<DefaultGameObjectKernel>().FromNewComponentOn(gameObject).AsSingle().NonLazy();
            }
            else
            {
                _container.Bind<MonoKernel>().FromInstance(kernel).AsSingle().NonLazy();
            }

            InstallSceneBindings(injectableMonoBehaviours);
            InstallInstallers();
        }
    }
}