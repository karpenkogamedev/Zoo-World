using System;
using UnityEngine;
using ZooWorld.Configs.Animals;
using ZooWorld.Infrastructure.Services;

namespace ZooWorld.Animals.Runtime
{
    public class AnimalPoolLifecycleCoordinator : IAnimalPoolLifecycleCoordinator
    {
        private readonly Transform _poolRoot;
        private readonly UpdateHandler _updateHandler;
        private readonly IAnimalPopulationService _animalPopulationService;
        private readonly IAnimalLifecycleService _animalLifecycleService;

        public AnimalPoolLifecycleCoordinator(
            Transform poolRoot,
            UpdateHandler updateHandler,
            IAnimalPopulationService animalPopulationService,
            IAnimalLifecycleService animalLifecycleService)
        {
            _poolRoot = poolRoot ?? throw new InvalidOperationException("Pool root is required.");
            _updateHandler = updateHandler;
            _animalPopulationService = animalPopulationService;
            _animalLifecycleService = animalLifecycleService;
        }

        public void PrepareForPooling(IAnimalController animal)
        {
            _animalLifecycleService.PrepareForPooling(animal, _poolRoot);
        }

        public void ActivateFromPool(IAnimalController animal, Vector3 position, AnimalData data)
        {
            _animalLifecycleService.Activate(animal, position, data.SpawnScale);
            RegisterActiveAnimal(animal);
        }

        public void ReleaseToPool(IAnimalController animal)
        {
            UnregisterActiveAnimal(animal);
            _animalLifecycleService.Deactivate(animal, _poolRoot);
        }

        public void Destroy(IAnimalController animal)
        {
            UnregisterActiveAnimal(animal);
            _animalLifecycleService.Destroy(animal);
        }

        private void RegisterActiveAnimal(IAnimalController animal)
        {
            _updateHandler.Register(animal);
            _animalPopulationService.Register(animal);
        }

        private void UnregisterActiveAnimal(IAnimalController animal)
        {
            _updateHandler.Unregister(animal);
            _animalPopulationService.Unregister(animal);
        }
    }
}
