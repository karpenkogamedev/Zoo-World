using UnityEngine;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Configs.Animals;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Animals.Movement
{
    public class LinearMovementStrategyBuilder : IMovementStrategyBuilder
    {
        private readonly Camera _camera;
        private readonly GameRuleConfig _gameRuleConfig;

        public LinearMovementStrategyBuilder(Camera camera, GameRuleConfig gameRuleConfig)
        {
            _camera = camera;
            _gameRuleConfig = gameRuleConfig;
        }

        public bool CanBuild(AnimalData data)
        {
            return data != null && data.MovementType == AnimalMovementType.Linear;
        }

        public IMovementStrategy Build(AnimalData data)
        {
            if (data == null)
                throw new System.InvalidOperationException("Linear movement requires AnimalData.");

            if (data.MovementType != AnimalMovementType.Linear)
                throw new System.InvalidOperationException($"Linear builder received incompatible movement type {data.MovementType} for {data.name}.");

            if (data.MovementConfig is not LinearMovementConfig linearConfig)
                throw new System.InvalidOperationException($"Linear movement config is not assigned correctly for {data.name}.");

            return new LinearMovementStrategy(_camera, _gameRuleConfig, linearConfig);
        }
    }
}
