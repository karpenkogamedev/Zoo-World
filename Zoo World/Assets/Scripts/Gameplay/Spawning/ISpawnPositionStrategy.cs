using UnityEngine;

namespace ZooWorld.Gameplay.Spawning
{
    public interface ISpawnPositionStrategy
    {
        Vector3 GetSpawnPosition();
    }
}
