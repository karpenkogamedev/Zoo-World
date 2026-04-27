using UnityEngine;
using ZooWorld.Animals.View;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Animals.Movement
{
    public class JumpMovementStrategy : DirectedMovementStrategyBase
    {
        private readonly JumpMovementConfig _config;
        private float _elapsedTime;
        private bool _isJumping;
        private float _jumpProgress;
        private float _jumpDuration;
        private Vector3 _jumpStartPosition;
        private Vector3 _jumpTargetPosition;
        private bool _forceNextJump;

        public JumpMovementStrategy(Camera camera, GameRuleConfig gameRuleConfig, JumpMovementConfig config) : base(camera, gameRuleConfig)
        {
            _config = config ?? throw new System.InvalidOperationException("Jump movement config is required.");
        }

        public override void Tick(IAnimalView view, float deltaTime)
        {
            EnsureDirection(Vector3.up);

            if (IsRecovering(deltaTime))
                return;

            Rigidbody rigidbody = GetRequiredRigidbody(view);
            Vector3 position = GetPosition(view);

            if (_isJumping)
            {
                UpdateJump(rigidbody, deltaTime);
                return;
            }

            float jumpInterval = Mathf.Max(_config.JumpInterval, Mathf.Epsilon);
            _elapsedTime += deltaTime;

            if (_forceNextJump == false && _elapsedTime < jumpInterval)
                return;

            if (_forceNextJump)
                _elapsedTime = 0f;
            else
                _elapsedTime -= jumpInterval;

            _forceNextJump = false;
            Direction = GetDirectionInsideBounds(position, Direction);
            Vector3 targetPosition = position + Direction * _config.JumpDistance;
            Direction = GetDirectionInsideBounds(targetPosition, Direction);
            targetPosition = position + Direction * _config.JumpDistance;

            StartJump(position, targetPosition);
        }

        protected override void OnReset()
        {
            _elapsedTime = 0f;
            _isJumping = false;
            _jumpProgress = 0f;
            _jumpDuration = 0f;
            _jumpStartPosition = Vector3.zero;
            _jumpTargetPosition = Vector3.zero;
            _forceNextJump = false;
        }

        protected override void OnRedirect()
        {
            _isJumping = false;
            _jumpProgress = 0f;
            _jumpDuration = 0f;
            _jumpStartPosition = Vector3.zero;
            _jumpTargetPosition = Vector3.zero;
            _forceNextJump = true;
        }

        private void StartJump(Vector3 startPosition, Vector3 targetPosition)
        {
            _isJumping = true;
            _jumpProgress = 0f;
            _jumpStartPosition = startPosition;
            _jumpTargetPosition = targetPosition;
            _jumpDuration = Mathf.Max(_config.JumpDistance / Mathf.Max(_config.LinearSpeed, 0.01f), 0.1f);
        }

        private void UpdateJump(Rigidbody rigidbody, float deltaTime)
        {
            _jumpProgress += deltaTime / _jumpDuration;

            float normalizedProgress = Mathf.Clamp01(_jumpProgress);
            Vector3 horizontalPosition = Vector3.Lerp(_jumpStartPosition, _jumpTargetPosition, normalizedProgress);
            float jumpHeight = Mathf.Max(_config.JumpHeight, 0.01f);
            float verticalOffset = 4f * jumpHeight * normalizedProgress * (1f - normalizedProgress);
            Vector3 nextPosition = new Vector3(horizontalPosition.x, _jumpStartPosition.y + verticalOffset, horizontalPosition.z);
            rigidbody.MovePosition(nextPosition);

            if (normalizedProgress < 1f)
                return;

            _isJumping = false;
            _jumpProgress = 0f;

            Vector3 landedPosition = _jumpTargetPosition;
            rigidbody.MovePosition(landedPosition);
        }
    }
}
