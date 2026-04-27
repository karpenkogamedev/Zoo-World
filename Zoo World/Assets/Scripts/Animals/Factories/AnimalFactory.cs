using UnityEngine;
using Zenject;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Animals.Runtime;
using ZooWorld.Animals.StateMachines;
using ZooWorld.Animals.View;
using ZooWorld.Configs.Animals;
using ZooWorld.Infrastructure.Services;

namespace ZooWorld.Animals.Factories
{
    public class AnimalFactory : IAnimalFactory, IAnimalInstanceFactory
    {
        private readonly IMovementFactory _movementFactory;
        private readonly IStateMachineFactory _stateMachineFactory;
        private readonly LazyInject<IAnimalPoolService> _animalPoolService;
        private readonly IAnimalFoodChainService _animalFoodChainService;
        private readonly IAnimalPopulationService _animalPopulationService;

        public AnimalFactory(
            IMovementFactory movementFactory,
            IStateMachineFactory stateMachineFactory,
            LazyInject<IAnimalPoolService> animalPoolService,
            IAnimalFoodChainService animalFoodChainService,
            IAnimalPopulationService animalPopulationService)
        {
            _movementFactory = movementFactory;
            _stateMachineFactory = stateMachineFactory;
            _animalPoolService = animalPoolService;
            _animalFoodChainService = animalFoodChainService;
            _animalPopulationService = animalPopulationService;
        }

        public bool TryCreate(AnimalData data, Vector3 position, out IAnimalController animal)
        {
            animal = null;

            if (_animalPopulationService.CanSpawn() == false)
                return false;

            return _animalPoolService.Value.TryGet(data, position, out animal);
        }

        public IAnimalController CreateInstance(AnimalData data, Vector3 position)
        {
            AnimalView view = Object.Instantiate(data.ViewPrefab, position, Quaternion.identity);
            IAnimalStateMachine stateMachine = _stateMachineFactory.Create();
            var movementStrategy = _movementFactory.Create(data);
            AnimalController animal = new AnimalController(
                data,
                view,
                movementStrategy,
                stateMachine,
                _animalPoolService.Value,
                _animalFoodChainService);

            _stateMachineFactory.Populate(stateMachine, animal, movementStrategy);
            return animal;
        }
    }
}
