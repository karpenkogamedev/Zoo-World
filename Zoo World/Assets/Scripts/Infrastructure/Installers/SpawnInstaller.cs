using UnityEngine;
using Zenject;
using ZooWorld.Gameplay.Spawning;

namespace ZooWorld.Infrastructure.Installers
{
    public class SpawnInstaller : MonoInstaller
    {
        [SerializeField] private Collider _spawnPlatform;

        public override void InstallBindings()
        {
            Container.Bind<ISpawnPositionStrategy>().To<PlatformSpawnPositionStrategy>().AsSingle().WithArguments(_spawnPlatform);
            Container.Bind<ISpawnIntervalStrategy>().To<RandomSpawnIntervalStrategy>().AsSingle();
            Container.Bind<IAnimalSpawnSelectionStrategy>().To<RandomAnimalSpawnSelectionStrategy>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnimalSpawnService>().AsSingle();
        }
    }
}
