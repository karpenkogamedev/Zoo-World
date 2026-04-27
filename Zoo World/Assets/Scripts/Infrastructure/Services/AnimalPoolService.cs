using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Animals.Runtime;
using ZooWorld.Configs.Animals;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Infrastructure.Services
{
    public class AnimalPoolService : IAnimalPoolService
    {
        private readonly IAnimalInstanceFactory _instanceFactory;
        private readonly IAnimalPoolLifecycleCoordinator _lifecycleCoordinator;
        private readonly GameRuleConfig _gameRuleConfig;
        private readonly Dictionary<AnimalData, ObjectPool<IAnimalController>> _pools = new();
        private readonly Dictionary<IAnimalController, AnimalData> _animalToData = new();

        public AnimalPoolService(
            IAnimalInstanceFactory instanceFactory,
            GameRuleConfig gameRuleConfig,
            IAnimalPoolLifecycleCoordinator lifecycleCoordinator)
        {
            _instanceFactory = instanceFactory;
            _gameRuleConfig = gameRuleConfig;
            _lifecycleCoordinator = lifecycleCoordinator;
        }

        public bool TryGet(AnimalData data, Vector3 position, out IAnimalController animal)
        {
            animal = null;

            ObjectPool<IAnimalController> pool = GetPool(data);
            animal = pool.Get();
            _lifecycleCoordinator.ActivateFromPool(animal, position, data);

            return true;
        }

        public void Release(IAnimalController animal)
        {
            if (animal == null)
                return;

            if (_animalToData.TryGetValue(animal, out AnimalData data) &&
                _pools.TryGetValue(data, out ObjectPool<IAnimalController> pool))
            {
                pool.Release(animal);
                return;
            }

            OnDestroyPooledObject(animal);
        }

        public void Clear()
        {
            List<IAnimalController> animals = new(_animalToData.Keys);

            foreach (IAnimalController animal in animals)
                OnDestroyPooledObject(animal);

            _pools.Clear();
            _animalToData.Clear();
        }

        private ObjectPool<IAnimalController> GetPool(AnimalData data)
        {
            if (_pools.TryGetValue(data, out ObjectPool<IAnimalController> pool))
                return pool;

            pool = new ObjectPool<IAnimalController>(
                createFunc: () => CreateAnimal(data),
                actionOnGet: _ => { },
                actionOnRelease: OnReleaseToPool,
                actionOnDestroy: OnDestroyPooledObject,
                collectionCheck: true,
                defaultCapacity: GetDefaultCapacity(),
                maxSize: GetMaxPoolSize());

            _pools[data] = pool;
            return pool;
        }

        private IAnimalController CreateAnimal(AnimalData data)
        {
            IAnimalController animal = _instanceFactory.CreateInstance(data, Vector3.zero);
            _animalToData[animal] = data;
            _lifecycleCoordinator.PrepareForPooling(animal);
            return animal;
        }

        private int GetDefaultCapacity()
        {
            return Mathf.Clamp(_gameRuleConfig.AnimalPoolDefaultCapacity, 1, GetMaxPoolSize());
        }

        private int GetMaxPoolSize()
        {
            return Mathf.Max(1, _gameRuleConfig.AnimalPoolMaxSize);
        }

        private void OnReleaseToPool(IAnimalController animal)
        {
            _lifecycleCoordinator.ReleaseToPool(animal);
        }

        private void OnDestroyPooledObject(IAnimalController animal)
        {
            _animalToData.Remove(animal);
            _lifecycleCoordinator.Destroy(animal);
        }
    }
}
