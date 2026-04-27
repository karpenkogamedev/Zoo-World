using System;

namespace ZooWorld.Gameplay.Spawning
{
    public interface ISpawnIntervalStrategy
    {
        bool IsRunning { get; }
        void Start(Action onTick);
        void Stop();
    }
}
