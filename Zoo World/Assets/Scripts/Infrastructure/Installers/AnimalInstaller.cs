using UnityEngine;
using Zenject;
using ZooWorld.Animals.Factories;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.StateMachines;

namespace ZooWorld.Infrastructure.Installers
{
    public class AnimalInstaller : MonoInstaller
    {
        [SerializeField] private Camera _worldCamera;

        public override void InstallBindings()
        {
            Container.Bind<IMovementStrategyBuilder>().To<JumpMovementStrategyBuilder>().AsTransient().WithArguments(_worldCamera);
            Container.Bind<IMovementStrategyBuilder>().To<LinearMovementStrategyBuilder>().AsTransient().WithArguments(_worldCamera);
            Container.Bind<IMovementFactory>().To<MovementFactory>().AsSingle();

            Container.Bind<IAnimalStateContributor>().To<AnimalIdleStateContributor>().AsTransient();
            Container.Bind<IAnimalStateContributor>().To<AnimalMovingStateContributor>().AsTransient();
            Container.Bind<IAnimalStateContributor>().To<AnimalDeadStateContributor>().AsTransient();
            Container.Bind<IStateMachineFactory>().To<StateMachineFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalFactory>().AsSingle();
        }
    }
}
