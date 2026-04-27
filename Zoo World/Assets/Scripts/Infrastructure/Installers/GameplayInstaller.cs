using UnityEngine;
using System;
using Zenject;
using ZooWorld.Animals.Runtime;
using ZooWorld.Gameplay.Statistics;
using ZooWorld.Infrastructure.Services;

namespace ZooWorld.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Transform _poolPoint;

        public override void InstallBindings()
        {
            if (_poolPoint == null)
                throw new InvalidOperationException("GameplayInstaller requires a pool point.");

            Container.BindInterfacesAndSelfTo<UpdateHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AnimalStatisticsService>().AsSingle();
            Container.Bind<IAnimalPopulationService>().To<AnimalPopulationService>().AsSingle();
            Container.Bind<IAnimalLifecycleService>().To<AnimalLifecycleService>().AsSingle();
            Container.Bind<IAnimalPoolLifecycleCoordinator>().To<AnimalPoolLifecycleCoordinator>().AsSingle().WithArguments(_poolPoint);
            Container.Bind<IAnimalPoolService>().To<AnimalPoolService>().AsSingle();
            Container.Bind<IAnimalFoodChainService>().To<AnimalFoodChainService>().AsSingle();
            Container.Bind<IAnimalInteractionSemanticsResolver>().To<AnimalInteractionSemanticsResolver>().AsSingle();
            Container.Bind<IAnimalMovementRedirectService>().To<AnimalMovementRedirectService>().AsSingle();
            Container.Bind<ICollisionRule>().To<PredatorVsPredatorRule>().AsTransient();
            Container.Bind<ICollisionRule>().To<PredatorEatsVictimRule>().AsTransient();
            Container.Bind<ICollisionRule>().To<PreyBounceRule>().AsTransient();
        }
    }
}
