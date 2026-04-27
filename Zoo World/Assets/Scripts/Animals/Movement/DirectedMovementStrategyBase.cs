using System;
using UnityEngine;
using ZooWorld.Animals.View;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Animals.Movement
{
    public abstract class DirectedMovementStrategyBase : IMovementStrategy, IResettableMovementStrategy, ICollisionResponsiveMovementStrategy
    {
        private readonly Camera _camera;
        private readonly CameraMovementBounds _cameraMovementBounds;
        private Vector3 _direction;
        private float _recoveryTimeRemaining;

        protected DirectedMovementStrategyBase(Camera camera, GameRuleConfig gameRuleConfig)
        {
            _camera = camera;
            _cameraMovementBounds = new CameraMovementBounds(gameRuleConfig);
        }

        public abstract void Tick(IAnimalView view, float deltaTime);

        public void Reset()
        {
            _direction = Vector3.zero;
            _recoveryTimeRemaining = 0f;
            OnReset();
        }

        public virtual void Redirect(Vector3 direction, float recoveryDuration)
        {
            _direction = direction == Vector3.zero ? Vector3.zero : direction.normalized;
            _recoveryTimeRemaining = Mathf.Max(0f, recoveryDuration);
            OnRedirect();
        }

        protected Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }

        protected void EnsureDirection(Vector3 fallbackDirection)
        {
            if (_direction != Vector3.zero)
                return;

            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;

            if (randomDirection == Vector2.zero)
                randomDirection = new Vector2(fallbackDirection.x, fallbackDirection.z).normalized;

            if (randomDirection == Vector2.zero)
                randomDirection = Vector2.right;

            _direction = new Vector3(randomDirection.x, 0f, randomDirection.y);
        }

        protected bool IsRecovering(float deltaTime)
        {
            if (_recoveryTimeRemaining <= 0f)
                return false;

            _recoveryTimeRemaining = Mathf.Max(0f, _recoveryTimeRemaining - deltaTime);
            return true;
        }

        protected Vector3 GetDirectionInsideBounds(Vector3 position, Vector3 direction)
        {
            return _cameraMovementBounds.GetDirectionInsideBounds(_camera, position, direction);
        }

        protected Vector3 GetPosition(IAnimalView view)
        {
            return GetRequiredRigidbody(view).position;
        }

        protected void MovePosition(IAnimalView view, Vector3 position)
        {
            GetRequiredRigidbody(view).MovePosition(position);
        }

        protected Rigidbody GetRequiredRigidbody(IAnimalView view)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            return view.Rigidbody;
        }

        protected virtual void OnReset()
        {
        }

        protected virtual void OnRedirect()
        {
        }
    }
}
