using System;
using UnityEngine;
using Zenject;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Infrastructure.Services;

namespace ZooWorld.Gameplay.Spawning
{
    public class AnimalSpawnService : IAnimalSpawnService, IInitializable, IDisposable
    {
        private readonly IAnimalFactory _animalFactory;
        private readonly IAnimalSpawnSelectionStrategy _animalSpawnSelectionStrategy;
        private readonly ISpawnPositionStrategy _spawnPositionStrategy;
        private readonly ISpawnIntervalStrategy _spawnIntervalStrategy;
        private readonly IAnimalPopulationService _animalPopulationService;

        public AnimalSpawnService(
            IAnimalFactory animalFactory,
            IAnimalSpawnSelectionStrategy animalSpawnSelectionStrategy,
            ISpawnPositionStrategy spawnPositionStrategy,
            ISpawnIntervalStrategy spawnIntervalStrategy,
            IAnimalPopulationService animalPopulationService)
        {
            _animalFactory = animalFactory;
            _animalSpawnSelectionStrategy = animalSpawnSelectionStrategy;
            _spawnPositionStrategy = spawnPositionStrategy;
            _spawnIntervalStrategy = spawnIntervalStrategy;
            _animalPopulationService = animalPopulationService;
        }

        public bool IsRunning => _spawnIntervalStrategy.IsRunning;

        public void Initialize()
        {
            Start();
        }

        public void Start()
        {
            if (IsRunning || _animalSpawnSelectionStrategy.HasAvailableAnimals == false)
                return;

            _spawnIntervalStrategy.Start(SpawnAnimal);
        }

        public void Stop()
        {
            _spawnIntervalStrategy.Stop();
        }

        public void Dispose()
        {
            Stop();
        }

        private void SpawnAnimal()
        {
            if (_animalSpawnSelectionStrategy.HasAvailableAnimals == false || _animalPopulationService.CanSpawn() == false)
                return;

            var data = _animalSpawnSelectionStrategy.GetNextAnimal();
            Vector3 spawnPosition = _spawnPositionStrategy.GetSpawnPosition();
            _animalFactory.TryCreate(data, spawnPosition, out _);
        }
    }
}
