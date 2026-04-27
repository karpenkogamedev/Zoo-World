using UnityEngine;
using ZooWorld.Animals.Movement;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public class AnimalMovementRedirectService : IAnimalMovementRedirectService
    {
        private readonly float _minRecoveryDuration = 0.15f;
        private readonly float _maxRecoveryDuration = 0.4f;
        private readonly float _recoveryDurationFactor = 0.05f;

        public void Redirect(IAnimalController animal, Vector3 direction, float impulse)
        {
            if (animal == null || animal.IsReleased)
                return;

            Vector3 normalizedDirection = Vector3.zero;

            if (direction != Vector3.zero)
                normalizedDirection = direction.normalized;

            float recoveryDuration = Mathf.Clamp(impulse * _recoveryDurationFactor, _minRecoveryDuration, _maxRecoveryDuration);

            if (animal is IAnimalMovementRuntime movementRuntime &&
                movementRuntime.MovementStrategy is ICollisionResponsiveMovementStrategy responsiveMovementStrategy)
                responsiveMovementStrategy.Redirect(normalizedDirection, recoveryDuration);

            Rigidbody rigidbody = animal.View.Rigidbody;
            if (normalizedDirection == Vector3.zero)
                return;

            rigidbody.velocity = normalizedDirection * impulse;
        }
    }
}
