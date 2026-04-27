namespace ZooWorld.Gameplay.Spawning
{
    public interface IAnimalSpawnService
    {
        bool IsRunning { get; }

        void Start();
        void Stop();
    }
}
