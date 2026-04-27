using ZooWorld.Animals.Movement;
using ZooWorld.Configs.Animals;

namespace ZooWorld.Animals.Factories.Interfaces
{
    public interface IMovementStrategyBuilder
    {
        bool CanBuild(AnimalData data);
        IMovementStrategy Build(AnimalData data);
    }
}
