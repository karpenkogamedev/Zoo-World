using UnityEngine;
using ZooWorld.Animals.View;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Animals.Movement
{
    public class LinearMovementStrategy : DirectedMovementStrategyBase
    {
        private readonly LinearMovementConfig _config;

        public LinearMovementStrategy(Camera camera, GameRuleConfig gameRuleConfig, LinearMovementConfig config) : base(camera, gameRuleConfig)
        {
            _config = config ?? throw new System.InvalidOperationException("Linear movement config is required.");
        }

        public override void Tick(IAnimalView view, float deltaTime)
        {
            EnsureDirection(Vector3.right);

            if (IsRecovering(deltaTime))
                return;

            Vector3 position = GetPosition(view);
            Direction = GetDirectionInsideBounds(position, Direction);
            position += Direction * (_config.Speed * deltaTime);
            Direction = GetDirectionInsideBounds(position, Direction);
            MovePosition(view, position);
        }
    }
}
