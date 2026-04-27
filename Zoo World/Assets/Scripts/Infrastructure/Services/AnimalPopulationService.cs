using System.Collections.Generic;
using ZooWorld.Animals.Runtime;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Infrastructure.Services
{
    public class AnimalPopulationService : IAnimalPopulationService
    {
        private readonly int _maxActiveAnimals;
        private readonly HashSet<IAnimalController> _activeAnimals = new();

        public AnimalPopulationService(GameRuleConfig gameRuleConfig)
        {
            _maxActiveAnimals = gameRuleConfig.MaxActiveAnimals;
        }

        public bool CanSpawn()
        {
            return _activeAnimals.Count < _maxActiveAnimals;
        }

        public void Register(IAnimalController animal)
        {
            if (animal != null)
                _activeAnimals.Add(animal);
        }

        public void Unregister(IAnimalController animal)
        {
            if (animal != null)
                _activeAnimals.Remove(animal);
        }
    }
}
