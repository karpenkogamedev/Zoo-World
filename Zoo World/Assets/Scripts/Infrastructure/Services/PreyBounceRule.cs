using UnityEngine;
using ZooWorld.Animals.Runtime;

namespace ZooWorld.Infrastructure.Services
{
    public class PreyBounceRule : ICollisionRule
    {
        public int Priority => 100;
        private readonly IAnimalMovementRedirectService _animalMovementRedirectService;
        private readonly IAnimalInteractionSemanticsResolver _semanticsResolver;

        public PreyBounceRule(
            IAnimalMovementRedirectService animalMovementRedirectService,
            IAnimalInteractionSemanticsResolver semanticsResolver)
        {
            _animalMovementRedirectService = animalMovementRedirectService;
            _semanticsResolver = semanticsResolver;
        }

        public bool CanApply(IAnimalController source, IAnimalController target)
        {
            return _semanticsResolver.CanBounceOnPreyCollision(source) &&
                   _semanticsResolver.CanBounceOnPreyCollision(target);
        }

        public void Apply(IAnimalController source, IAnimalController target)
        {
            Vector3 sourceDirection = GetSeparationDirection(source, target);
            Vector3 targetDirection = -sourceDirection;

            _animalMovementRedirectService.Redirect(source, sourceDirection, source.Data.CollisionImpulse);
            _animalMovementRedirectService.Redirect(target, targetDirection, target.Data.CollisionImpulse);
        }

        private Vector3 GetSeparationDirection(IAnimalController source, IAnimalController target)
        {
            Vector3 direction = source.View.Transform.position - target.View.Transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > Mathf.Epsilon)
                return direction.normalized;

            Vector2 randomDirection = Random.insideUnitCircle.normalized;

            if (randomDirection == Vector2.zero)
                randomDirection = Vector2.right;

            return new Vector3(randomDirection.x, 0f, randomDirection.y);
        }
    }
}
