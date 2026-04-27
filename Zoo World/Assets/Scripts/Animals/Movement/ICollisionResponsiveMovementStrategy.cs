using UnityEngine;

namespace ZooWorld.Animals.Movement
{
    public interface ICollisionResponsiveMovementStrategy
    {
        void Redirect(Vector3 direction, float recoveryDuration);
    }
}
