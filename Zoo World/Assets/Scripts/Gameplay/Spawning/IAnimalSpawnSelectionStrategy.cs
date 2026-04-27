using ZooWorld.Configs.Animals;

namespace ZooWorld.Gameplay.Spawning
{
    public interface IAnimalSpawnSelectionStrategy
    {
        bool HasAvailableAnimals { get; }
        AnimalData GetNextAnimal();
    }
}
