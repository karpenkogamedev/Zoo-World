using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZooWorld.Gameplay.Spawning
{
    public class PlatformSpawnPositionStrategy : ISpawnPositionStrategy
    {
        private readonly int _maxSpawnPositionAttempts = 16;
        private readonly float _spawnHeightOffset = 0.5f;
        private readonly Collider _platformCollider;

        public PlatformSpawnPositionStrategy(Collider platformCollider)
        {
            _platformCollider = platformCollider ?? throw new InvalidOperationException("Spawn platform collider is required.");
        }

        public Vector3 GetSpawnPosition()
        {
            Bounds bounds = _platformCollider.bounds;
            float raycastPadding = bounds.extents.y + _spawnHeightOffset;
            float rayOriginY = bounds.max.y + raycastPadding;
            float rayDistance = rayOriginY - bounds.min.y + _spawnHeightOffset;

            foreach (int _ in System.Linq.Enumerable.Range(0, _maxSpawnPositionAttempts))
            {
                float x = Random.Range(bounds.min.x, bounds.max.x);
                float z = Random.Range(bounds.min.z, bounds.max.z);
                Ray ray = new Ray(new Vector3(x, rayOriginY, z), Vector3.down);

                if (_platformCollider.Raycast(ray, out RaycastHit hit, rayDistance))
                    return ApplySpawnHeightOffset(hit.point);
            }

            Vector3 fallbackOrigin = new Vector3(bounds.center.x, rayOriginY, bounds.center.z);
            Ray fallbackRay = new Ray(fallbackOrigin, Vector3.down);

            if (_platformCollider.Raycast(fallbackRay, out RaycastHit fallbackHit, rayDistance))
                return ApplySpawnHeightOffset(fallbackHit.point);

            return ApplySpawnHeightOffset(_platformCollider.ClosestPoint(bounds.center));
        }

        private Vector3 ApplySpawnHeightOffset(Vector3 point)
        {
            return point + Vector3.up * _spawnHeightOffset;
        }
    }
}
