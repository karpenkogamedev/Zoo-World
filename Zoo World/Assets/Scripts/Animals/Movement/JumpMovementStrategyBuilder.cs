using UnityEngine;
using ZooWorld.Animals.Factories.Interfaces;
using ZooWorld.Configs.Animals;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Animals.Movement
{
    public class JumpMovementStrategyBuilder : IMovementStrategyBuilder
    {
        private readonly Camera _camera;
        private readonly GameRuleConfig _gameRuleConfig;

        public JumpMovementStrategyBuilder(Camera camera, GameRuleConfig gameRuleConfig)
        {
            _camera = camera;
            _gameRuleConfig = gameRuleConfig;
        }

        public bool CanBuild(AnimalData data)
        {
            return data != null && data.MovementType == AnimalMovementType.Jump;
        }

        public IMovementStrategy Build(AnimalData data)
        {
            if (data == null)
                throw new System.InvalidOperationException("Jump movement requires AnimalData.");

            if (data.MovementType != AnimalMovementType.Jump)
                throw new System.InvalidOperationException($"Jump builder received incompatible movement type {data.MovementType} for {data.name}.");

            if (data.MovementConfig is not JumpMovementConfig jumpConfig)
                throw new System.InvalidOperationException($"Jump movement config is not assigned correctly for {data.name}.");

            return new JumpMovementStrategy(
                _camera,
                _gameRuleConfig,
                jumpConfig);
        }
    }
}
