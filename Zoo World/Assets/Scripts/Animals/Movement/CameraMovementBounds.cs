using UnityEngine;
using ZooWorld.Configs.Gameplay;

namespace ZooWorld.Animals.Movement
{
    public class CameraMovementBounds
    {
        private readonly GameRuleConfig _gameRuleConfig;

        public CameraMovementBounds(GameRuleConfig gameRuleConfig)
        {
            _gameRuleConfig = gameRuleConfig;
        }

        public Vector3 GetDirectionInsideBounds(Camera camera, Vector3 position, Vector3 direction)
        {
            if (camera == null)
                return direction;

            Vector3 viewportPoint = camera.WorldToViewportPoint(position);
            float minViewportX = _gameRuleConfig.MinViewportX;
            float maxViewportX = _gameRuleConfig.MaxViewportX;
            float minViewportY = _gameRuleConfig.MinViewportY;
            float maxViewportY = _gameRuleConfig.MaxViewportY;

            if (viewportPoint.x >= minViewportX && viewportPoint.x <= maxViewportX &&
                viewportPoint.y >= minViewportY && viewportPoint.y <= maxViewportY)
                return direction;

            float targetViewportX = Mathf.Clamp(viewportPoint.x, minViewportX, maxViewportX);
            float targetViewportY = Mathf.Clamp(viewportPoint.y, minViewportY, maxViewportY);
            Vector3 targetPoint = camera.ViewportToWorldPoint(new Vector3(targetViewportX, targetViewportY, viewportPoint.z));
            Vector3 returnDirection = targetPoint - position;
            returnDirection.y = 0f;

            if (returnDirection == Vector3.zero)
                return direction;

            return returnDirection.normalized;
        }
    }
}
