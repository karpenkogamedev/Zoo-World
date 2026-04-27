using System.Collections.Generic;
using UnityEngine;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Gameplay.Spawning
{
    public class RandomAnimalSpawnSelectionStrategy : IAnimalSpawnSelectionStrategy
    {
        private readonly IReadOnlyList<AnimalData> _animalCatalog;

        public RandomAnimalSpawnSelectionStrategy(IReadOnlyList<AnimalData> animalCatalog)
        {
            _animalCatalog = animalCatalog;
        }

        public bool HasAvailableAnimals => _animalCatalog.Count > 0;

        public AnimalData GetNextAnimal()
        {
            return _animalCatalog[Random.Range(0, _animalCatalog.Count)];
        }
    }
}
